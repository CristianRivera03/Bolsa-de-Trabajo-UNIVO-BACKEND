using System;
using System.Collections.Generic;

namespace PortalTrabajo.Model;

public partial class CatSector
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    
    public virtual ICollection<Empresa> Empresas { get; set; } = new List<Empresa>();
}
