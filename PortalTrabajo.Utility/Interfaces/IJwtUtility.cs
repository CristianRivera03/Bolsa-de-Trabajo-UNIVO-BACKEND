using PortalTrabajo.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Text;
namespace PortalTrabajo.Utility.Interfaces
{
    public interface IJwtUtility
    {
        /// <summary>
        /// Generates an authentication JSON Web Token (JWT) based on the user session details.
        /// </summary>
        /// <param name="session">The session DTO containing user data.</param>
        /// <returns>A signed JWT string.</returns>
        string GenerarJWT(SessionDTO session);

        /// <summary>
        /// Generates a short-lived recovery token specifically designed for resetting passwords.
        /// </summary>
        /// <param name="usuarioId">The ID of the user requesting the reset.</param>
        /// <returns>A signed JWT string with a password reset claim.</returns>
        string GenerarTokenRecuperacion(int usuarioId);

        /// <summary>
        /// Validates a recovery token and extracts the user ID if the token is valid and unexpired.
        /// </summary>
        /// <param name="token">The JWT recovery token.</param>
        /// <returns>The extracted user ID.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if the token is invalid, expired, or not meant for recovery.</exception>
        int ValidarTokenRecuperacion(string token);
    }
}
