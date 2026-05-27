using System;
using System.Collections.Generic;
namespace PortalTrabajo.Model;
public partial class Habilidade
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public virtual ICollection<EstudianteHabilidade> EstudianteHabilidades { get; set; } = new List<EstudianteHabilidade>();
    public virtual ICollection<OfertaHabilidade> OfertaHabilidades { get; set; } = new List<OfertaHabilidade>();
}
