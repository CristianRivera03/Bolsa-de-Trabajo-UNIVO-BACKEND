using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalTrabajo.API.Utility;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DTO.Alumnos;
using PortalTrabajo.DTO.Auth;
using System;
using System.Threading.Tasks;

namespace PortalTrabajo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private readonly IAlumnoService _alumnoService;

        public AlumnoController(IAlumnoService alumnoService)
        {
            _alumnoService = alumnoService;
        }

        [HttpPost("consultar")]
        public async Task<IActionResult> Consultar([FromBody] VerificarAlumnoDTO model)
        {
            var rsp = new Response<AlumnoActivoDTO>();
            try
            {
                var alumno = await _alumnoService.Consultar(model);
                rsp.status = true;
                rsp.value = alumno;
                return Ok(rsp);
            }
            catch (UnauthorizedAccessException ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return Unauthorized(rsp);
            }
            catch (InvalidOperationException ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return Conflict(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, rsp);
            }
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistroEstudianteDTO model)
        {
            var rsp = new Response<SessionDTO>();
            try
            {
                var session = await _alumnoService.Registrar(model);
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
            catch (InvalidOperationException ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return Conflict(rsp);
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
