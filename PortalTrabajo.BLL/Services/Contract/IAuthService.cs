using PortalTrabajo.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortalTrabajo.BLL.Services.Contract
{
    public interface IAuthService
    {
        Task<SessionDTO> Login(string email, string password);
    }
}
