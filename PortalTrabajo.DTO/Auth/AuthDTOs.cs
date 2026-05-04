using System;

namespace PortalTrabajo.DTO.Auth
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SessionDTO
    {
        public int UsuarioId { get; set; }
        public string Email { get; set; }
        public string RolName { get; set; }
        public string Token { get; set; }
    }
}
