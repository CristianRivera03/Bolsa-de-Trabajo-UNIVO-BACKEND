using System;
using Microsoft.EntityFrameworkCore;
using PortalTrabajo.Model;
using System.Linq;

namespace PortalTrabajo.DAL.DBContext
{
    public partial class PortalTrabajoDbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            // Global Query Filters para Soft Deletes
            // Filtra automáticamente todos los registros donde Activo sea falso
            
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var isActiveProperty = entityType.FindProperty("Activo");
                if (isActiveProperty != null && isActiveProperty.ClrType == typeof(bool))
                {
                    // Obtiene el tipo CLR de la entidad
                    var entityClrType = entityType.ClrType;

                    // Busca el método genérico AddGlobalFilter en esta clase
                    var method = typeof(PortalTrabajoDbContext)
                        .GetMethod(nameof(AddGlobalFilter), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                    // Lo invoca genéricamente para el tipo actual
                    var genericMethod = method?.MakeGenericMethod(entityClrType);
                    genericMethod?.Invoke(this, new object[] { modelBuilder });
                }
            }
        }

        private void AddGlobalFilter<T>(ModelBuilder modelBuilder) where T : class
        {
            modelBuilder.Entity<T>().HasQueryFilter(e => EF.Property<bool>(e, "Activo") == true);
        }
    }
}
