using System;
using System.Collections.Generic;

namespace PortalTrabajo.Model;

public partial class OfertasLaborale
{
    public int Id { get; set; }

    public int EmpresaId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string? Requisitos { get; set; }

    public int ModalidadId { get; set; }

    public string? Ubicacion { get; set; }

    public decimal? SalarioMin { get; set; }

    public decimal? SalarioMax { get; set; }

    public DateTime? FechaPublicacion { get; set; }

    public DateTime? FechaExpiracion { get; set; }

    public bool? Activa { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual Empresa Empresa { get; set; } = null!;

    public virtual CatModalidade Modalidad { get; set; } = null!;

    public virtual ICollection<OfertaHabilidade> OfertaHabilidades { get; set; } = new List<OfertaHabilidade>();
}
