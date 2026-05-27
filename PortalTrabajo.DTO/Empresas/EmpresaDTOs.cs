using System;
namespace PortalTrabajo.DTO.Empresas
{
    public class ContactoEmpresaDTO
    {
        public int? Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Cargo { get; set; }
        public string? Dui { get; set; }
        public string? TelefonoMovil { get; set; }
        public string? CorreoContacto { get; set; }
    }
    public class EmpresaDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string NombreComercial { get; set; }
        public string Sector { get; set; }
        public string Descripcion { get; set; }
        public string SitioWeb { get; set; }
        public string LogoUrl { get; set; }
        public string RazonSocial { get; set; }
        public string Nit { get; set; }
        public string Direccion { get; set; }
        public string TelefonoFijo { get; set; }
        public string CorreoInstitucional { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public ContactoEmpresaDTO Contacto { get; set; }
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
        public string? SitioWeb { get; set; }
        public string? LogoUrl { get; set; }
        public string? RazonSocial { get; set; }
        public string? Nit { get; set; }
        public string? Direccion { get; set; }
        public string? TelefonoFijo { get; set; }
        public string? CorreoInstitucional { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public ContactoEmpresaDTO Contacto { get; set; }
    }
}
