using System;
using System.Collections.Generic;

namespace PortalTrabajo.Model;

public partial class CatCarrera
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Facultad { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public int? UsuarioCreacionId { get; set; }

    public virtual ICollection<OfertaCarrera> OfertaCarreras { get; set; } = new List<OfertaCarrera>();

    public virtual ICollection<PerfilesEstudiante> PerfilesEstudiantes { get; set; } = new List<PerfilesEstudiante>();
}
