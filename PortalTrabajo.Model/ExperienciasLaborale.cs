using System;
using System.Collections.Generic;
namespace PortalTrabajo.Model;
public partial class ExperienciasLaborale
{
    public int Id { get; set; }
    public int PerfilId { get; set; }
    public string Empresa { get; set; } = null!;
    public string Cargo { get; set; } = null!;
    public DateOnly FechaInicio { get; set; }
    public DateOnly? FechaFin { get; set; }
    public bool? EsTrabajoActual { get; set; }
    public string? DescripcionPuesto { get; set; }
    public virtual PerfilesEstudiante Perfil { get; set; } = null!;
}
