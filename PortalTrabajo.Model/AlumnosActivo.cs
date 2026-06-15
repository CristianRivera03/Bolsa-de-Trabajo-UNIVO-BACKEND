using System;
using System.Collections.Generic;

namespace PortalTrabajo.Model;

public partial class AlumnosActivo
{
    public int Id { get; set; }

    public string Carnet { get; set; } = null!;

    public string PasswordPortal { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Genero { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string? Carrera { get; set; }

    public string? Facultad { get; set; }

    public string? EstadoAcademico { get; set; }
}
