using System;
using System.Collections.Generic;
namespace PortalTrabajo.Model;
public partial class CatEstadosPostulacion
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public virtual ICollection<Postulacione> Postulaciones { get; set; } = new List<Postulacione>();
}
