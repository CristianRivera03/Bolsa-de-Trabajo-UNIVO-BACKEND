using System;
using System.Collections.Generic;
namespace PortalTrabajo.Model;
public partial class CatMunicipio
{
    public int Id { get; set; }
    public int DepartamentoId { get; set; }
    public string Nombre { get; set; } = null!;
    public virtual ICollection<CatDistrito> CatDistritos { get; set; } = new List<CatDistrito>();
    public virtual CatDepartamento Departamento { get; set; } = null!;
}
