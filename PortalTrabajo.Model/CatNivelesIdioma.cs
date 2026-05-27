using System;
using System.Collections.Generic;
namespace PortalTrabajo.Model;
public partial class CatNivelesIdioma
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public virtual ICollection<EstudianteIdioma> EstudianteIdiomas { get; set; } = new List<EstudianteIdioma>();
}
