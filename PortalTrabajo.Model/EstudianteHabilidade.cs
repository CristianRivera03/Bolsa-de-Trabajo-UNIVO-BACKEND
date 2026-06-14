using System;
using System.Collections.Generic;

namespace PortalTrabajo.Model;

public partial class EstudianteHabilidade
{
    public int PerfilId { get; set; }

    public int HabilidadId { get; set; }

    public int? NivelDominio { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public int? UsuarioCreacionId { get; set; }

    public virtual Habilidade Habilidad { get; set; } = null!;

    public virtual PerfilesEstudiante Perfil { get; set; } = null!;
}
