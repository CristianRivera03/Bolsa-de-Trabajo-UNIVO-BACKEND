using System;

namespace PortalTrabajo.DTO.OfertasLaborales
{
    public class OfertaLaboralDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
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
        public bool? Activa { get; set; }
        public List<string> Carreras { get; set; } = new List<string>();
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
        public List<int> CarreraIds { get; set; } = new List<int>();
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
        public bool? Activa { get; set; }
        public List<int> CarreraIds { get; set; } = new List<int>();
    }
}
