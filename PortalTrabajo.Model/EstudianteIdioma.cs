using System;
using System.Collections.Generic;

namespace PortalTrabajo.Model;

public partial class EstudianteIdioma
{
    public int Id { get; set; }

    public int PerfilId { get; set; }

    public string Idioma { get; set; } = null!;

    public int NivelId { get; set; }

    public virtual CatNivelesIdioma Nivel { get; set; } = null!;

    public virtual PerfilesEstudiante Perfil { get; set; } = null!;
}
