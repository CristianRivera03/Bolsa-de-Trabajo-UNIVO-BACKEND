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

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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

    }
}
