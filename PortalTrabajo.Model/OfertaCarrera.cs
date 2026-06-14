using System;
using System.Collections.Generic;

namespace PortalTrabajo.Model;

public partial class OfertaCarrera
{
    public int OfertaId { get; set; }

    public int CarreraId { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? UsuarioCreacionId { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public bool Activo { get; set; }

    public virtual CatCarrera Carrera { get; set; } = null!;

    public virtual OfertasLaborale Oferta { get; set; } = null!;
}
