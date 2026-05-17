using PortalTrabajo.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortalTrabajo.Utility.Interfaces
{
    public interface IJwtUtility
    {
        string GenerarJWT(SessionDTO session);
    }
}
