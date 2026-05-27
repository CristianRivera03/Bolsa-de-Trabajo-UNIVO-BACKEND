using System;
using System.Collections.Generic;
namespace PortalTrabajo.Model;
public partial class CatCarrera
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Facultad { get; set; }
    public bool Activa { get; set; }
    public virtual ICollection<PerfilesEstudiante> PerfilesEstudiantes { get; set; } = new List<PerfilesEstudiante>();
    public virtual ICollection<OfertasLaborale> Oferta { get; set; } = new List<OfertasLaborale>();
}
