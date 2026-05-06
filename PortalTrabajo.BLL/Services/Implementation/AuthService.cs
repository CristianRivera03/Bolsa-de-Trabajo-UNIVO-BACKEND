using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DAL.Repositories.Contract;
using PortalTrabajo.DTO.Auth;
using PortalTrabajo.Model;
using PortalTrabajo.Utility;
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

        public AuthService(IGenericRepository<Usuario> userRepository, IMapper mapper, ILogger<IAuthService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
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

                return _mapper.Map<SessionDTO>(userFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error login");
                throw;
            }
        }
    }
}
