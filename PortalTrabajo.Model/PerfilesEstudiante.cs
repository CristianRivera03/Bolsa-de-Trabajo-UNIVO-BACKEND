using System;
using System.Collections.Generic;

namespace PortalTrabajo.Model;

public partial class PerfilesEstudiante
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public string Carnet { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string? Genero { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? SobreMi { get; set; }

    public string? FotoUrl { get; set; }

    public string? EnlaceGitHub { get; set; }

    public string? EnlaceLinkedIn { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public int? CarreraId { get; set; }

    public bool BuscaEmpleo { get; set; }

    public virtual CatCarrera? Carrera { get; set; }

    public virtual ICollection<Educacion> Educacions { get; set; } = new List<Educacion>();

    public virtual ICollection<EstudianteHabilidade> EstudianteHabilidades { get; set; } = new List<EstudianteHabilidade>();

    public virtual ICollection<EstudianteIdioma> EstudianteIdiomas { get; set; } = new List<EstudianteIdioma>();

    public virtual ICollection<ExperienciasLaborale> ExperienciasLaborales { get; set; } = new List<ExperienciasLaborale>();

    public virtual ICollection<ProyectosEstudiante> ProyectosEstudiantes { get; set; } = new List<ProyectosEstudiante>();

    public virtual Usuario Usuario { get; set; } = null!;
}
