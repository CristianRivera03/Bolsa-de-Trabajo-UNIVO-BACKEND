using System;
using System.Collections.Generic;
namespace PortalTrabajo.Model;
public partial class CatTiposLicencium
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public virtual ICollection<OfertasLaborale> OfertasLaborales { get; set; } = new List<OfertasLaborale>();
}
