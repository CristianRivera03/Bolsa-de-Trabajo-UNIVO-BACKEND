using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DTO.PerfilesEstudiante;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PortalTrabajo.API.Controllers
{
    [Route("api/PerfilEstudiante/Idiomas")]
    [Authorize]
    [ApiController]
    public class EstudianteIdiomaController : ControllerBase
    {
        private readonly IEstudianteIdiomaService _idiomaService;

        public EstudianteIdiomaController(IEstudianteIdiomaService idiomaService)
        {
            _idiomaService = idiomaService;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] EstudianteIdiomaDTO dto)
        {
            try
            {
                var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(claimId, out int usuarioId))
                    return Unauthorized(new { status = false, msg = "Token inválido." });

                var resultado = await _idiomaService.AddIdiomaAsync(usuarioId, dto);
                return Ok(new { status = resultado, msg = "Idioma guardado con éxito." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, msg = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] EstudianteIdiomaDTO dto)
        {
            try
            {
                var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(claimId, out int usuarioId))
                    return Unauthorized(new { status = false, msg = "Token inválido." });

                var resultado = await _idiomaService.UpdateIdiomaAsync(usuarioId, id, dto);
                return Ok(new { status = resultado, msg = "Idioma actualizado con éxito." });
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

                var resultado = await _idiomaService.DeleteIdiomaAsync(usuarioId, id);
                return Ok(new { status = resultado, msg = "Idioma eliminado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, msg = ex.Message });
            }
        }
    }
}