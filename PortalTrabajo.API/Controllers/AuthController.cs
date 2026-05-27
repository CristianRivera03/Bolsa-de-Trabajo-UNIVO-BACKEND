using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalTrabajo.API.Utility;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DTO.Auth;
namespace PortalTrabajo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly PortalTrabajo.DAL.Repositories.Contract.IGenericRepository<PortalTrabajo.Model.Usuario> _usuarioRepo;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthService authService, PortalTrabajo.DAL.Repositories.Contract.IGenericRepository<PortalTrabajo.Model.Usuario> usuarioRepo, IConfiguration configuration)
        {
            _authService = authService;
            _usuarioRepo = usuarioRepo;
            _configuration = configuration;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var rsp = new Response<SessionDTO>();
            try
            {
                var session = await _authService.Login(model.Email, model.Password);
                rsp.status = true;
                rsp.value = session;
                return Ok(rsp);
            }
            catch (UnauthorizedAccessException ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return Unauthorized(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, rsp);
            }
        }
        [HttpPost("SetupInitialAdmin")]
        public async Task<IActionResult> SetupInitialAdmin([FromBody] CrearAdminDTO modelo)
        {
            var rsp = new Response<string>();
            try
            {
                var usuarioExistente = await _usuarioRepo.Get(u => u.Email == modelo.Email);
                if (usuarioExistente != null)
                {
                    rsp.status = false;
                    rsp.msg = "Ya existe un usuario con este correo.";
                    return BadRequest(rsp);
                }
                var nuevoAdmin = new PortalTrabajo.Model.Usuario
                {
                    Email = modelo.Email,
                    PasswordHash = PortalTrabajo.Utility.SecurityHelper.HashPassword(modelo.Password),
                    RolId = 1, // Administrador
                    Activo = true,
                    FechaRegistro = DateTime.Now
                };
                var resultado = await _usuarioRepo.Create(nuevoAdmin);
                if (resultado != null && resultado.Id > 0)
                {
                    rsp.status = true;
                    rsp.value = "Administrador maestro creado exitosamente.";
                    return Ok(rsp);
                }
                rsp.status = false;
                rsp.msg = "No se pudo crear el administrador.";
                return BadRequest(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = "Error del servidor: " + ex.Message;
                return StatusCode(500, rsp);
            }
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] RecuperarPasswordDTO model)
        {
            var rsp = new Response<string>();
            try
            {
                var frontendUrl = _configuration["FrontendUrl"] ?? "http://localhost:4200";
                await _authService.SolicitarRecuperacionPassword(model.Email, frontendUrl);
                rsp.status = true;
                rsp.msg = "Si el correo está registrado, recibirás un enlace de recuperación.";
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = "Ocurrió un error al procesar la solicitud.";
                return StatusCode(StatusCodes.Status500InternalServerError, rsp);
            }
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] RestablecerPasswordDTO model)
        {
            var rsp = new Response<string>();
            try
            {
                await _authService.RestablecerPassword(model.Token, model.NuevaPassword);
                rsp.status = true;
                rsp.msg = "Contraseña restablecida exitosamente.";
                return Ok(rsp);
            }
            catch (UnauthorizedAccessException ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return BadRequest(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = "Ocurrió un error al restablecer la contraseña.";
                return StatusCode(StatusCodes.Status500InternalServerError, rsp);
            }
        }
    }
}
