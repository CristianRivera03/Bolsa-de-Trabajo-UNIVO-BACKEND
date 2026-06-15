using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PortalTrabajo.DTO.Auth;
using PortalTrabajo.Model;
using PortalTrabajo.Utility.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
namespace PortalTrabajo.Utility
{
    public class JwtUtility : IJwtUtility
    {
        private readonly JwtSettings _jwtSettings;
        public JwtUtility(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// Generates an authentication JSON Web Token (JWT) based on the user session details.
        /// </summary>
        /// <param name="session">The session DTO containing user data.</param>
        /// <returns>A signed JWT string.</returns>
        public string GenerarJWT(SessionDTO session)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, session.UsuarioId.ToString()),
                new Claim(ClaimTypes.Email, session.Email),
                new Claim(ClaimTypes.Role, session.RolName),
                new Claim(ClaimTypes.Name, session.NombreCompleto)
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Generates a short-lived recovery token specifically designed for resetting passwords.
        /// </summary>
        /// <param name="usuarioId">The ID of the user requesting the reset.</param>
        /// <returns>A signed JWT string with a password reset claim.</returns>
        public string GenerarTokenRecuperacion(int usuarioId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var claims = new List<Claim>
            {
                new Claim("userId", usuarioId.ToString()),
                new Claim("Purpose", "PasswordReset")
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Validates a recovery token and extracts the user ID if the token is valid and unexpired.
        /// </summary>
        /// <param name="token">The JWT recovery token.</param>
        /// <returns>The extracted user ID.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if the token is invalid, expired, or not meant for recovery.</exception>
        public int ValidarTokenRecuperacion(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var purposeClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "Purpose")?.Value;
                if (purposeClaim != "PasswordReset")
                {
                    throw new UnauthorizedAccessException("Token no es válido para recuperación de contraseña.");
                }
                var userIdString = jwtToken.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;
                if (userIdString == null)
                {
                    throw new UnauthorizedAccessException("El token no contiene el ID de usuario.");
                }
                return int.Parse(userIdString);
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException($"Token inválido o expirado. Detalles: {ex.Message}");
            }
        }
    }
}
