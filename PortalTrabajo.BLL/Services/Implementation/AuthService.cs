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

        public AuthService(IGenericRepository<Usuario> userRepository, IMapper mapper, ILogger<IAuthService> logger, IJwtUtility jwtUtility)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _jwtUtility = jwtUtility;
        }

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
    }
}
