using AutoMapper;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DAL.Repositories.Contract;
using PortalTrabajo.DTO.Alumnos;
using PortalTrabajo.DTO.Auth;
using PortalTrabajo.Model;
using PortalTrabajo.Utility;
using PortalTrabajo.Utility.Interfaces;
using System;
using System.Threading.Tasks;
namespace PortalTrabajo.BLL.Services.Implementation
{
    public class AlumnoService : IAlumnoService
    {
        private readonly IGenericRepository<AlumnosActivo> _alumnoRepo;
        private readonly IGenericRepository<Usuario> _usuarioRepo;
        private readonly IGenericRepository<PerfilesEstudiante> _perfilRepo;
        private readonly IGenericRepository<CatRole> _rolRepo;
        private readonly IGenericRepository<CatCarrera> _carreraRepo;
        private readonly IMapper _mapper;
        private readonly IJwtUtility _jwtUtility;
        public AlumnoService(
            IGenericRepository<AlumnosActivo> alumnoRepo,
            IGenericRepository<Usuario> usuarioRepo,
            IGenericRepository<PerfilesEstudiante> perfilRepo,
            IGenericRepository<CatRole> rolRepo,
            IGenericRepository<CatCarrera> carreraRepo,
            IMapper mapper,
            IJwtUtility jwtUtility)
        {
            _alumnoRepo = alumnoRepo;
            _usuarioRepo = usuarioRepo;
            _perfilRepo = perfilRepo;
            _rolRepo = rolRepo;
            _carreraRepo = carreraRepo;
            _mapper = mapper;
            _jwtUtility = jwtUtility;
        }
        public async Task<AlumnoActivoDTO> Consultar(VerificarAlumnoDTO model)
        {
            var alumnoDb = await _alumnoRepo.Get(a => a.Carnet == model.Carnet && a.PasswordPortal == model.PasswordPortal);
            if (alumnoDb == null)
            {
                throw new UnauthorizedAccessException("Credenciales incorrectas o carnet no encontrado en el portal de la universidad.");
            }
            var emailGenerado = $"{model.Carnet}@univo.edu.sv".ToLower();
            var yaRegistrado = await _usuarioRepo.Exists(u => u.Email == emailGenerado);
            if (yaRegistrado)
            {
                throw new InvalidOperationException("El alumno ya se encuentra registrado en la bolsa de trabajo. Por favor, inicia sesión.");
            }
            return _mapper.Map<AlumnoActivoDTO>(alumnoDb);
        }
        public async Task<SessionDTO> Registrar(RegistroEstudianteDTO model)
        {
            var alumnoDb = await _alumnoRepo.Get(a => a.Carnet == model.Carnet && a.PasswordPortal == model.PasswordPortal);
            if (alumnoDb == null)
            {
                throw new UnauthorizedAccessException("Credenciales incorrectas.");
            }
            var emailGenerado = $"{model.Carnet}@univo.edu.sv".ToLower();
            var yaRegistrado = await _usuarioRepo.Exists(u => u.Email == emailGenerado);
            if (yaRegistrado)
            {
                throw new InvalidOperationException("El alumno ya se encuentra registrado en la bolsa de trabajo.");
            }
            var rol = await _rolRepo.Get(r => r.Nombre == "Estudiante" || r.Nombre == "Alumno");
            int rolId = rol != null ? rol.Id : 2; // Fallback a 2 si no lo encuentra por nombre
            var nuevoUsuario = new Usuario
            {
                Email = emailGenerado,
                PasswordHash = SecurityHelper.HashPassword(model.PasswordPortal),
                RolId = rolId,
                Activo = true,
                FechaRegistro = DateTime.Now
            };
            await _usuarioRepo.Create(nuevoUsuario);
            var carreraStr = alumnoDb.Carrera;
            int? carreraIdAsignada = null;
            if (!string.IsNullOrEmpty(carreraStr))
            {
                var carreraDb = await _carreraRepo.Get(c => c.Nombre.Contains(carreraStr) || carreraStr.Contains(c.Nombre));
                if (carreraDb != null)
                {
                    carreraIdAsignada = carreraDb.Id;
                }
            }
            var nuevoPerfil = new PerfilesEstudiante
            {
                UsuarioId = nuevoUsuario.Id,
                Carnet = alumnoDb.Carnet,
                Nombres = alumnoDb.Nombres,
                Apellidos = alumnoDb.Apellidos,
                Genero = alumnoDb.Genero,
                CarreraId = carreraIdAsignada,
                BuscaEmpleo = true
            };
            await _perfilRepo.Create(nuevoPerfil);
            var session = new SessionDTO
            {
                UsuarioId = nuevoUsuario.Id,
                Email = nuevoUsuario.Email,
                RolName = rol?.Nombre ?? "Estudiante",
                NombreCompleto = $"{alumnoDb.Nombres} {alumnoDb.Apellidos}"
            };
            session.Token = _jwtUtility.GenerarJWT(session);
            return session;
        }
    }
}
