using System;
namespace PortalTrabajo.DTO.OfertasLaborales
{
    public class OfertaLaboralDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string? Requisitos { get; set; }
        public int EmpresaId { get; set; }
        public string EmpresaNombre { get; set; }
        public string EmpresaLogoUrl { get; set; }
        public int ModalidadId { get; set; }
        public string ModalidadNombre { get; set; }
        public string Ubicacion { get; set; }
        public decimal? SalarioMin { get; set; }
        public decimal? SalarioMax { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public bool Activo { get; set; }
        public int? Vacantes { get; set; }
        public int? EdadMin { get; set; }
        public int? EdadMax { get; set; }
        public bool? TieneVehiculo { get; set; }
        public int? LicenciaId { get; set; }
        public string LicenciaNombre { get; set; }
        public int? TipoContratoId { get; set; }
        public string TipoContratoNombre { get; set; }
        public int? DistritoId { get; set; }
        public string DistritoNombre { get; set; }
        public int? GeneroId { get; set; }
        public string GeneroNombre { get; set; }
        public List<string> Carreras { get; set; } = new List<string>();
        public List<OfertaHabilidadDTO> Habilidades { get; set; } = new List<OfertaHabilidadDTO>();
    }
    public class OfertaHabilidadDTO
    {
        public int HabilidadId { get; set; }
        public string NombreHabilidad { get; set; }
        public bool EsObligatorio { get; set; }
    }
    public class OfertaLaboralCreateDTO
    {
        public int EmpresaId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Requisitos { get; set; }
        public int ModalidadId { get; set; }
        public string Ubicacion { get; set; }
        public decimal? SalarioMin { get; set; }
        public decimal? SalarioMax { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public int? Vacantes { get; set; }
        public int? EdadMin { get; set; }
        public int? EdadMax { get; set; }
        public bool? TieneVehiculo { get; set; }
        public int? LicenciaId { get; set; }
        public int? TipoContratoId { get; set; }
        public int? DistritoId { get; set; }
        public int? GeneroId { get; set; }
        public List<int> CarreraIds { get; set; } = new List<int>();
        public List<int> HabilidadIds { get; set; } = new List<int>();
    }
    public class OfertaLaboralUpdateDTO
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int ModalidadId { get; set; }
        public string Ubicacion { get; set; }
        public decimal? SalarioMin { get; set; }
        public decimal? SalarioMax { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public bool Activo { get; set; }
        public int? Vacantes { get; set; }
        public int? EdadMin { get; set; }
        public int? EdadMax { get; set; }
        public bool? TieneVehiculo { get; set; }
        public int? LicenciaId { get; set; }
        public int? TipoContratoId { get; set; }
        public int? DistritoId { get; set; }
        public int? GeneroId { get; set; }
        public List<int> CarreraIds { get; set; } = new List<int>();
        public List<int> HabilidadIds { get; set; } = new List<int>();
    }
}

