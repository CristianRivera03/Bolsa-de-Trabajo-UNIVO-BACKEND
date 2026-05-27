using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DTO.PerfilesEstudiante;
using System.Security.Claims;
namespace PortalTrabajo.API.Controllers
{
    [Route("api/PerfilEstudiante/Experiencia")]
    [Authorize]
    [ApiController]
    public class ExperenciaLaboralController : ControllerBase
    {
        private readonly IExperienciaLaboralService _experienciaService;
        public ExperenciaLaboralController(IExperienciaLaboralService experienciaService)
        {
            _experienciaService = experienciaService;
        }
        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] ExperienciaLaboralDTO dto)
        {
            try
            {
                var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(claimId, out int usuarioId))
                    return Unauthorized(new { status = false, msg = "Token inválido." });
                var resultado = await _experienciaService.AddExperienciaAsync(usuarioId, dto);
                return Ok(new { status = resultado, msg = "Experiencia guardada con éxito." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, msg = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] ExperienciaLaboralDTO dto)
        {
            try
            {
                var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(claimId, out int usuarioId))
                    return Unauthorized(new { status = false, msg = "Token inválido." });
                var resultado = await _experienciaService.UpdateExperienciaAsync(usuarioId, id, dto);
                return Ok(new { status = resultado, msg = "Experiencia actualizada con éxito." });
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
                var resultado = await _experienciaService.DeleteExperienciaAsync(usuarioId, id);
                return Ok(new { status = resultado, msg = "Experiencia eliminada." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, msg = ex.Message });
            }
        }
    }
}
