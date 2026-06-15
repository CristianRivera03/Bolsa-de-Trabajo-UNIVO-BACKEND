using System;
using System.Collections.Generic;

namespace PortalTrabajo.Model;

public partial class Empresa
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public string NombreComercial { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? SitioWeb { get; set; }

    public string? LogoUrl { get; set; }

    public string? RazonSocial { get; set; }

    public string? Nit { get; set; }

    public string? TelefonoFijo { get; set; }

    public string? CorreoInstitucional { get; set; }

    public string? Facebook { get; set; }

    public string? Twitter { get; set; }

    public int? SectorId { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public int? UsuarioCreacionId { get; set; }

    public int? DistritoId { get; set; }

    public virtual ContactosEmpresa? ContactosEmpresa { get; set; }

    public virtual CatDistrito? Distrito { get; set; }

    public virtual ICollection<OfertasLaborale> OfertasLaborales { get; set; } = new List<OfertasLaborale>();

    public virtual CatSectore? Sector { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
