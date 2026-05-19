using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DTO.PerfilesEstudiante;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PortalTrabajo.API.Controllers
{
    [Route("api/PerfilEstudiante/Proyectos")] 
    [Authorize]
    [ApiController]
    public class ProyectoEstudianteController : ControllerBase
    {
        private readonly IProyectosService _proyectoService;

        public ProyectoEstudianteController(IProyectosService proyectoService)
        {
            _proyectoService = proyectoService;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] ProyectoEstudianteDTO dto)
        {
            try
            {
                var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(claimId, out int usuarioId))
                    return Unauthorized(new { status = false, msg = "Token inválido." });

                var resultado = await _proyectoService.AddProyectoAsync(usuarioId, dto);
                return Ok(new { status = resultado, msg = "Proyecto guardado con éxito." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, msg = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] ProyectoEstudianteDTO dto)
        {
            try
            {
                var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(claimId, out int usuarioId))
                    return Unauthorized(new { status = false, msg = "Token inválido." });

                var resultado = await _proyectoService.UpdateProyectoAsync(usuarioId, id, dto);
                return Ok(new { status = resultado, msg = "Proyecto actualizado con éxito." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, msg = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(claimId, out int usuarioId))
                    return Unauthorized(new { status = false, msg = "Token inválido." });

                var resultado = await _proyectoService.DeleteProyectoAsync(usuarioId, id);
                return Ok(new { status = resultado, msg = "Proyecto eliminado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, msg = ex.Message });
            }
        }
    }
}