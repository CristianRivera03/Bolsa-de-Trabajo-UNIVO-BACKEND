using System;
using System.Collections.Generic;

namespace PortalTrabajo.Model;

public partial class CatDistrito
{
    public int Id { get; set; }

    public int MunicipioId { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public int? UsuarioCreacionId { get; set; }

    public virtual ICollection<Empresa> Empresas { get; set; } = new List<Empresa>();

    public virtual CatMunicipio Municipio { get; set; } = null!;

    public virtual ICollection<OfertasLaborale> OfertasLaborales { get; set; } = new List<OfertasLaborale>();

    public virtual ICollection<PerfilesEstudiante> PerfilesEstudiantes { get; set; } = new List<PerfilesEstudiante>();
}
