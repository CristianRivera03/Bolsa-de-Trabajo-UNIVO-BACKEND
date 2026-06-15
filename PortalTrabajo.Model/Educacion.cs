using System;
using System.Collections.Generic;

namespace PortalTrabajo.Model;

public partial class Educacion
{
    public int Id { get; set; }

    public int PerfilId { get; set; }

    public int GradoAcademicoId { get; set; }

    public string Institucion { get; set; } = null!;

    public string TituloObtenido { get; set; } = null!;

    public DateOnly FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public string? Estado { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public int? UsuarioCreacionId { get; set; }

    public virtual CatGradosAcademico GradoAcademico { get; set; } = null!;

    public virtual PerfilesEstudiante Perfil { get; set; } = null!;
}
