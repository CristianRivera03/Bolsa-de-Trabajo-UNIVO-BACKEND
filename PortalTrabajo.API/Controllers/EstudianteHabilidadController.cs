using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DTO.PerfilesEstudiante;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
namespace PortalTrabajo.API.Controllers
{
    [Route("api/PerfilEstudiante/Habilidades")]
    [Authorize(Roles = "Estudiante,Alumno")]
    [ApiController]
    public class EstudianteHabilidadController : ControllerBase
    {
        private readonly IEstudianteHabilidadService _habilidadService;
        public EstudianteHabilidadController(IEstudianteHabilidadService habilidadService)
        {
            _habilidadService = habilidadService;
        }
        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] EstudianteHabilidadDTO dto)
        {
            try
            {
                var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(claimId, out int usuarioId))
                    return Unauthorized(new { status = false, msg = "Token inválido." });
                var resultado = await _habilidadService.AddHabilidadAsync(usuarioId, dto);
                return Ok(new { status = resultado, msg = "Habilidad agregada con éxito." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, msg = ex.Message });
            }
        }
        [HttpPut("{habilidadId}")]
        public async Task<IActionResult> Editar(int habilidadId, [FromBody] EstudianteHabilidadDTO dto)
        {
            try
            {
                var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(claimId, out int usuarioId))
                    return Unauthorized(new { status = false, msg = "Token inválido." });
                var resultado = await _habilidadService.UpdateHabilidadAsync(usuarioId, habilidadId, dto);
                return Ok(new { status = resultado, msg = "Habilidad actualizada con éxito." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, msg = ex.Message });
            }
        }
        [HttpDelete("{habilidadId}")]
        public async Task<IActionResult> Eliminar(int habilidadId)
        {
            try
            {
                var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(claimId, out int usuarioId))
                    return Unauthorized(new { status = false, msg = "Token inválido." });
                var resultado = await _habilidadService.DeleteHabilidadAsync(usuarioId, habilidadId);
                return Ok(new { status = resultado, msg = "Habilidad eliminada." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, msg = ex.Message });
            }
        }
    }
}
