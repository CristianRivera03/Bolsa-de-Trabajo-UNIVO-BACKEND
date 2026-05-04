using System;
using System.Collections.Generic;

namespace PortalTrabajo.Model;

public partial class Empresa
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public string NombreComercial { get; set; } = null!;

    public string? Sector { get; set; }

    public string? Descripcion { get; set; }

    public string? SitioWeb { get; set; }

    public string? LogoUrl { get; set; }

    public virtual ICollection<OfertasLaborale> OfertasLaborales { get; set; } = new List<OfertasLaborale>();

    public virtual Usuario Usuario { get; set; } = null!;
}
