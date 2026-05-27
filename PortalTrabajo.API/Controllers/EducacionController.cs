using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DTO.PerfilesEstudiante;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
namespace PortalTrabajo.API.Controllers
{
    [Route("api/PerfilEstudiante/Educacion")]
    [Authorize]
    [ApiController]
    public class EducacionController : ControllerBase
    {
        private readonly IEducacionService _educacionService;
        public EducacionController(IEducacionService educacionService)
        {
            _educacionService = educacionService;
        }
        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] EducacionDTO dto)
        {
            try
            {
                var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(claimId, out int usuarioId))
                    return Unauthorized(new { status = false, msg = "Token inválido." });
                var resultado = await _educacionService.AddEducacionAsync(usuarioId, dto);
                return Ok(new { status = resultado, msg = "Educación guardada con éxito." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, msg = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] EducacionDTO dto)
        {
            try
            {
                var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(claimId, out int usuarioId))
                    return Unauthorized(new { status = false, msg = "Token inválido." });
                var resultado = await _educacionService.UpdateEducacionAsync(usuarioId, id, dto);
                return Ok(new { status = resultado, msg = "Educación actualizada con éxito." });
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
                var resultado = await _educacionService.DeleteEducacionAsync(usuarioId, id);
                return Ok(new { status = resultado, msg = "Educación eliminada." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, msg = ex.Message });
            }
        }
    }
}
