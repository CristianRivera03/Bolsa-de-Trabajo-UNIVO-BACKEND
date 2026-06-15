using System;
using System.Collections.Generic;

namespace PortalTrabajo.Model;

public partial class Postulacione
{
    public int Id { get; set; }

    public int OfertaId { get; set; }

    public int PerfilId { get; set; }

    public DateTime FechaPostulacion { get; set; }

    public int EstadoId { get; set; }

    public string? Mensaje { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public int? UsuarioCreacionId { get; set; }

    public virtual CatEstadosPostulacion Estado { get; set; } = null!;

    public virtual OfertasLaborale Oferta { get; set; } = null!;

    public virtual PerfilesEstudiante Perfil { get; set; } = null!;
}
