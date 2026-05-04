using System;
using System.Collections.Generic;

namespace PortalTrabajo.Model;

public partial class ProyectosEstudiante
{
    public int Id { get; set; }

    public int PerfilId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string? TecnologiasUsadas { get; set; }

    public string? EnlaceRepositorio { get; set; }

    public DateOnly? FechaProyecto { get; set; }

    public virtual PerfilesEstudiante Perfil { get; set; } = null!;
}
