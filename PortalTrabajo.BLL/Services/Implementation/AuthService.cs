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

                // Enviar notificación de inicio de sesión sin IP
                EnviarNotificacionLogin(userFound.Email);

                return session;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error login");
                throw;
            }
        }

        private void EnviarNotificacionLogin(string email)
        {
            Task.Run(async () =>
            {
                try
                {
                    string fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    string asunto = "Alerta de Inicio de Sesión - Portal de Trabajo UNIVO";
                    string cuerpo = $@"
                        <p>Se ha detectado un nuevo inicio de sesión en tu cuenta.</p>
                        <ul>
                            <li><strong>Fecha y Hora:</strong> {fecha}</li>
                        </ul>
                        <p>Si fuiste tú, no es necesario realizar ninguna acción.</p>
                        <div class='alert-box'>Si no reconoces esta actividad, te recomendamos cambiar tu contraseña inmediatamente.</div>";

                    string htmlFinal = PortalTrabajo.Utility.EmailTemplateBuilder.BuildTemplate("Nuevo Inicio de Sesión Detectado", cuerpo);
                    await _emailService.EnviarCorreoAsync(email, asunto, htmlFinal);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al enviar email de login.");
                }
            });
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
            string urlRecuperacion = $"{frontendUrl}/reset-password?token={token}";
                
            string emailHtml = $@"
                <p>Hola,</p>
                <p>Recibimos una solicitud para restablecer tu contraseña. Haz clic en el botón de abajo para continuar:</p>
                <p style='text-align: center;'>
                    <a href='{urlRecuperacion}' class='btn'>Restablecer Contraseña</a>
                </p>
                <p>Si no solicitaste este cambio, puedes ignorar este correo.</p>";

            string htmlFinal = PortalTrabajo.Utility.EmailTemplateBuilder.BuildTemplate("Recuperación de Contraseña", emailHtml);
            await _emailService.EnviarCorreoAsync(email, "Restablece tu contraseña - Bolsa de Trabajo UNIVO", htmlFinal);
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
