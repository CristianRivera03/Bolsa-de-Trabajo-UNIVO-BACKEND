using PortalTrabajo.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Text;
namespace PortalTrabajo.BLL.Services.Contract
{
    public interface IAuthService
    {
        /// <summary>
        /// Authenticates a user based on email and password.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>A session object containing user details and the JWT token.</returns>
        Task<SessionDTO> Login(string email, string password);

        /// <summary>
        /// Initiates the password recovery process by generating a token and sending a recovery email.
        /// </summary>
        /// <param name="email">The email address associated with the account.</param>
        /// <param name="frontendUrl">The base URL of the frontend application to construct the recovery link.</param>
        Task SolicitarRecuperacionPassword(string email, string frontendUrl);

        /// <summary>
        /// Resets the user's password using a valid recovery token.
        /// </summary>
        /// <param name="token">The JWT recovery token.</param>
        /// <param name="nuevaPassword">The new password to set.</param>
        Task RestablecerPassword(string token, string nuevaPassword);
    }
}
