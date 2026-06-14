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

    public DateTime? FechaModificacion { get; set; }

    public int? Vacantes { get; set; }

    public int? EdadMin { get; set; }

    public int? EdadMax { get; set; }

    public bool? TieneVehiculo { get; set; }

    public int? LicenciaId { get; set; }

    public int? TipoContratoId { get; set; }

    public int? GeneroId { get; set; }

    public int? DistritoId { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? UsuarioModificacionId { get; set; }

    public int? UsuarioCreacionId { get; set; }

    public virtual CatDistrito? Distrito { get; set; }

    public virtual Empresa Empresa { get; set; } = null!;

    public virtual CatGenero? Genero { get; set; }

    public virtual CatTiposLicencium? Licencia { get; set; }

    public virtual CatModalidade Modalidad { get; set; } = null!;

    public virtual ICollection<OfertaCarrera> OfertaCarreras { get; set; } = new List<OfertaCarrera>();

    public virtual ICollection<OfertaHabilidade> OfertaHabilidades { get; set; } = new List<OfertaHabilidade>();

    public virtual ICollection<Postulacione> Postulaciones { get; set; } = new List<Postulacione>();

    public virtual CatTiposContrato? TipoContrato { get; set; }
}
