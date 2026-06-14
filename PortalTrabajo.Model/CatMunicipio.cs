using System;
using System.Collections.Generic;

namespace PortalTrabajo.Model;

public partial class CatMunicipio
{
    public int Id { get; set; }

    public int DepartamentoId { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public int? UsuarioCreacionId { get; set; }

    public virtual ICollection<CatDistrito> CatDistritos { get; set; } = new List<CatDistrito>();

    public virtual CatDepartamento Departamento { get; set; } = null!;
}
