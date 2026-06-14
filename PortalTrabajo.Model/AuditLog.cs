using System;
using System.Collections.Generic;

namespace PortalTrabajo.Model;

public partial class AuditLog
{
    public int Id { get; set; }

    public string NombreTabla { get; set; } = null!;

    public string Accion { get; set; } = null!;

    public string RegistroId { get; set; } = null!;

    public string? ValoresAntiguos { get; set; }

    public string? ValoresNuevos { get; set; }

    public DateTime Fecha { get; set; }

    public int? UsuarioId { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? UsuarioCreacionId { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public bool Activo { get; set; }
}
