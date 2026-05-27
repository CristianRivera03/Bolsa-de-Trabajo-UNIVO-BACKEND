using System;
namespace PortalTrabajo.DTO.Usuarios
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string RolName { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
    public class UsuarioCreateDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int RolId { get; set; }
    }
}
