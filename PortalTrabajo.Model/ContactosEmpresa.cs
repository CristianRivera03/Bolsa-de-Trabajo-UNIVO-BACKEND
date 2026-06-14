using System;
using System.Collections.Generic;

namespace PortalTrabajo.Model;

public partial class ContactosEmpresa
{
    public int Id { get; set; }

    public int EmpresaId { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string Cargo { get; set; } = null!;

    public string Dui { get; set; } = null!;

    public string TelefonoMovil { get; set; } = null!;

    public string CorreoContacto { get; set; } = null!;

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public int? UsuarioCreacionId { get; set; }

    public virtual Empresa Empresa { get; set; } = null!;
}
