using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.Model;

namespace PortalTrabajo.IOC.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;

        // Tablas que NO se auditan para evitar recursividad
        private static readonly HashSet<string> _excludedTables = new(StringComparer.OrdinalIgnoreCase)
        {
            nameof(AuditLog)
        };

        public AuditableEntityInterceptor(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            var userId = _currentUserService.GetCurrentUserId();
            var now = DateTime.Now;

            var auditEntries = new List<AuditLog>();

            foreach (var entry in context.ChangeTracker.Entries().ToList())
            {
                // Excluir el propio AuditLog y entidades sin cambios
                var tableName = entry.Metadata.GetTableName() ?? entry.Metadata.Name;
                if (_excludedTables.Contains(tableName)) continue;
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged) continue;

                if (entry.State == EntityState.Added)
                {
                    // Rellenar campos de auditoría en la entidad nueva
                    var createdAtProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "FechaCreacion");
                    if (createdAtProp != null && (createdAtProp.CurrentValue == null || (DateTime)createdAtProp.CurrentValue == DateTime.MinValue))
                        createdAtProp.CurrentValue = now;

                    var createdByProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "UsuarioCreacionId");
                    if (createdByProp != null && createdByProp.CurrentValue == null)
                        createdByProp.CurrentValue = userId;

                    var activoProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "Activo");
                    if (activoProp != null && activoProp.Metadata.ClrType == typeof(bool))
                        activoProp.CurrentValue = true;

                    // Registro en bitácora
                    var nuevosValores = entry.Properties
                        .Where(p => p.Metadata.Name != "FechaCreacion" && p.Metadata.Name != "FechaModificacion"
                                 && p.Metadata.Name != "UsuarioCreacionId" && p.Metadata.Name != "UsuarioModificacionId")
                        .ToDictionary(p => p.Metadata.Name, p => p.CurrentValue);

                    auditEntries.Add(new AuditLog
                    {
                        NombreTabla = tableName,
                        Accion = "Creación",
                        RegistroId = GetPrimaryKey(entry),
                        ValoresNuevos = JsonSerializer.Serialize(nuevosValores),
                        ValoresAntiguos = null,
                        Fecha = now,
                        UsuarioId = userId,
                        FechaCreacion = now,
                        UsuarioCreacionId = userId,
                        Activo = true
                    });
                }
                else if (entry.State == EntityState.Modified)
                {
                    // Rellenar campos de auditoría
                    var updatedAtProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "FechaModificacion");
                    if (updatedAtProp != null) updatedAtProp.CurrentValue = now;

                    var updatedByProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "UsuarioModificacionId");
                    if (updatedByProp != null) updatedByProp.CurrentValue = userId;

                    // Capturar sólo las propiedades que realmente cambiaron
                    var antiguas = entry.Properties
                        .Where(p => p.IsModified && !IsAuditField(p.Metadata.Name))
                        .ToDictionary(p => p.Metadata.Name, p => p.OriginalValue);

                    var nuevas = entry.Properties
                        .Where(p => p.IsModified && !IsAuditField(p.Metadata.Name))
                        .ToDictionary(p => p.Metadata.Name, p => p.CurrentValue);

                    // Solo registrar si hubo cambios reales (no sólo los campos de auditoría)
                    if (antiguas.Any())
                    {
                        auditEntries.Add(new AuditLog
                        {
                            NombreTabla = tableName,
                            Accion = "Modificación",
                            RegistroId = GetPrimaryKey(entry),
                            ValoresAntiguos = JsonSerializer.Serialize(antiguas),
                            ValoresNuevos = JsonSerializer.Serialize(nuevas),
                            Fecha = now,
                            UsuarioId = userId,
                            FechaCreacion = now,
                            UsuarioCreacionId = userId,
                            Activo = true
                        });
                    }
                }
                else if (entry.State == EntityState.Deleted)
                {
                    // Soft delete: cambiar a Modified y poner Activo = false
                    var activoProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "Activo");
                    if (activoProp != null && activoProp.Metadata.ClrType == typeof(bool))
                    {
                        entry.State = EntityState.Modified;
                        activoProp.CurrentValue = false;

                        var updatedAtProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "FechaModificacion");
                        if (updatedAtProp != null) updatedAtProp.CurrentValue = now;

                        var updatedByProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "UsuarioModificacionId");
                        if (updatedByProp != null) updatedByProp.CurrentValue = userId;
                    }

                    auditEntries.Add(new AuditLog
                    {
                        NombreTabla = tableName,
                        Accion = "Eliminación",
                        RegistroId = GetPrimaryKey(entry),
                        ValoresAntiguos = JsonSerializer.Serialize(
                            entry.Properties.ToDictionary(p => p.Metadata.Name, p => p.OriginalValue)),
                        ValoresNuevos = null,
                        Fecha = now,
                        UsuarioId = userId,
                        FechaCreacion = now,
                        UsuarioCreacionId = userId,
                        Activo = true
                    });
                }
            }

            // Insertar todos los registros de auditoría de una sola vez
            if (auditEntries.Any())
            {
                context.Set<AuditLog>().AddRange(auditEntries);
            }
        }

        private static string GetPrimaryKey(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
        {
            var keyValues = entry.Metadata.FindPrimaryKey()?.Properties
                .Select(p => entry.Property(p.Name).CurrentValue?.ToString())
                ?? Enumerable.Empty<string?>();
            return string.Join(",", keyValues);
        }

        private static bool IsAuditField(string name) =>
            name is "FechaCreacion" or "FechaModificacion" or "UsuarioCreacionId" or "UsuarioModificacionId";
    }
}
