using System;

namespace PortalTrabajo.DTO.Empresas
{
    public class EmpresaDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string NombreComercial { get; set; }
        public string Sector { get; set; }
        public string Descripcion { get; set; }
        public string SitioWeb { get; set; }
        public string LogoUrl { get; set; }
    }

    public class EmpresaCreateDTO
    {
        public string Email { get; set; } 
        public string Password { get; set; }
        public string NombreComercial { get; set; }
        public string Sector { get; set; }
        public string Descripcion { get; set; }
        public string SitioWeb { get; set; }
    }

    public class EmpresaUpdateDTO
    {
        public string NombreComercial { get; set; }
        public string Sector { get; set; }
        public string Descripcion { get; set; }
        public string SitioWeb { get; set; }
        public string LogoUrl { get; set; }
    }
}
