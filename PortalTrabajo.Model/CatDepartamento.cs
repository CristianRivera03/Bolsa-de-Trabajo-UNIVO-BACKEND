using System;
using System.Collections.Generic;
namespace PortalTrabajo.Model;
public partial class CatDepartamento
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public virtual ICollection<CatMunicipio> CatMunicipios { get; set; } = new List<CatMunicipio>();
}
