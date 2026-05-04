using System;
using System.Collections.Generic;

namespace PortalTrabajo.Model;

public partial class CatGradosAcademico
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Educacion> Educacions { get; set; } = new List<Educacion>();
}
