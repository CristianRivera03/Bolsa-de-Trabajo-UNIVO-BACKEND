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

namespace PortalTrabajo.Utility
{
    public class JwtUtility : IJwtUtility
    {
        private readonly JwtSettings _jwtSettings;

        public JwtUtility(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

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
    }
}
