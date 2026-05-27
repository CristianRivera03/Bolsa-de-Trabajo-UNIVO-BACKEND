using System;
namespace PortalTrabajo.DTO.Admin
{
    public class AdminDashboardStatsDTO
    {
        public int TotalUsuarios { get; set; }
        public int UsuariosActivos { get; set; }
        public int UsuariosInactivos { get; set; }
        public int TotalEmpresas { get; set; }
        public int EmpresasActivas { get; set; }
        public int EmpresasInactivas { get; set; }
        public int TotalOfertas { get; set; }
        public int OfertasActivas { get; set; }
        public int OfertasInactivas { get; set; }
        public int TotalPostulaciones { get; set; }
    }
    public class AdminEmpresaDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string NombreComercial { get; set; } = null!;
        public string? RazonSocial { get; set; }
        public string? Nit { get; set; }
        public string Email { get; set; } = null!;
        public bool Activo { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
