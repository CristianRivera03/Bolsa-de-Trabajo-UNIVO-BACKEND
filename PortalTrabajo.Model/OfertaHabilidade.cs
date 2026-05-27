using System;
using System.Collections.Generic;
namespace PortalTrabajo.Model;
public partial class OfertaHabilidade
{
    public int OfertaId { get; set; }
    public int HabilidadId { get; set; }
    public bool? EsObligatorio { get; set; }
    public virtual Habilidade Habilidad { get; set; } = null!;
    public virtual OfertasLaborale Oferta { get; set; } = null!;
}
