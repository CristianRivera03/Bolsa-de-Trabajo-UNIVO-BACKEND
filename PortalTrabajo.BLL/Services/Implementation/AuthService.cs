using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DAL.Repositories.Contract;
using PortalTrabajo.DTO.Auth;
using PortalTrabajo.Model;
using PortalTrabajo.Utility;
using PortalTrabajo.Utility.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
namespace PortalTrabajo.BLL.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<Usuario> _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<IAuthService> _logger;
        private readonly IJwtUtility _jwtUtility;
        private readonly IEmailService _emailService;
        public AuthService(IGenericRepository<Usuario> userRepository, IMapper mapper, ILogger<IAuthService> logger, IJwtUtility jwtUtility, IEmailService emailService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _jwtUtility = jwtUtility;
            _emailService = emailService;
        }
        /// <summary>
        /// Authenticates the user and returns a session containing a JWT token.
        /// </summary>
        /// <param name="email">User's registered email.</param>
        /// <param name="password">User's password in plain text.</param>
        /// <returns>A SessionDTO containing the generated JWT and user basic details.</returns>
        public async Task<SessionDTO> Login(string email, string password)
        {
            try
            {
                var queryUser = _userRepository.Query(u => u.Email == email && u.Activo == true);
                var userFound = await queryUser
                    .Include(u => u.Rol)
                    .Include(u => u.PerfilesEstudiante)
                    .Include(u => u.Empresa)
                    .FirstOrDefaultAsync();
                if (userFound == null || !SecurityHelper.VerifyPassword(password, userFound.PasswordHash))
                {
                    throw new UnauthorizedAccessException("El usuario no existe o la contraseña es incorrecta");
                }
                var session = _mapper.Map<SessionDTO>(userFound);
                session.Token = _jwtUtility.GenerarJWT(session);
                return session;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error login");
                throw;
            }
        }

        /// <summary>
        /// Initiates the password recovery process by generating a token and sending it via email.
        /// To prevent user enumeration attacks, it completes silently if the user is not found.
        /// </summary>
        /// <param name="email">The email associated with the account.</param>
        /// <param name="frontendUrl">The frontend base URL to build the recovery link.</param>
        public async Task SolicitarRecuperacionPassword(string email, string frontendUrl)
        {
            var user = await _userRepository.Get(u => u.Email == email && u.Activo == true);
            if (user == null)
            {
                return;
            }
            var token = _jwtUtility.GenerarTokenRecuperacion(user.Id);
            var resetLink = $"{frontendUrl}/auth/reset-password?token={token}";
            var emailHtml = $@"
            <div style='font-family: Arial, sans-serif; background-color: #f3f4f6; padding: 40px 0;'>
                <div style='max-w-md; margin: 0 auto; background-color: #ffffff; border-radius: 12px; padding: 30px; box-shadow: 0 4px 6px rgba(0,0,0,0.1); text-align: center;'>
                    <h2 style='color: #4f46e5; font-size: 24px; font-weight: 800; margin-bottom: 20px;'>UNIVO</h2>
                    <h3 style='color: #1f2937; font-size: 18px; margin-bottom: 15px;'>Recuperación de Contraseña</h3>
                    <p style='color: #4b5563; font-size: 14px; margin-bottom: 25px; line-height: 1.5;'>
                        Hemos recibido una solicitud para restablecer la contraseña de tu cuenta. 
                        Haz clic en el botón de abajo para asignar una nueva contraseña. Este enlace expira en 15 minutos.
                    </p>
                    <a href='{resetLink}' style='display: inline-block; background-color: #4f46e5; color: #ffffff; text-decoration: none; padding: 12px 25px; border-radius: 8px; font-weight: bold; font-size: 14px;'>Restablecer Contraseña</a>
                    <p style='color: #9ca3af; font-size: 12px; margin-top: 30px;'>
                        Si no solicitaste este cambio, puedes ignorar este correo de forma segura.
                    </p>
                </div>
            </div>";
            await _emailService.EnviarCorreoAsync(email, "Restablece tu contraseña - Bolsa de Trabajo UNIVO", emailHtml);
        }

        /// <summary>
        /// Validates the provided token and updates the user's password with the newly provided hash.
        /// </summary>
        /// <param name="token">The JWT token sent to the user's email.</param>
        /// <param name="nuevaPassword">The new password to set.</param>
        public async Task RestablecerPassword(string token, string nuevaPassword)
        {
            try
            {
                var userId = _jwtUtility.ValidarTokenRecuperacion(token);
                var user = await _userRepository.GetById(userId);
                if (user == null || user.Activo == false)
                {
                    throw new UnauthorizedAccessException("Usuario no encontrado o inactivo.");
                }
                user.PasswordHash = SecurityHelper.HashPassword(nuevaPassword);
                var result = await _userRepository.Update(user);
                if (!result)
                {
                    throw new Exception("Error al actualizar la contraseña en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al restablecer contraseña.");
                throw;
            }
        }
    }
}
