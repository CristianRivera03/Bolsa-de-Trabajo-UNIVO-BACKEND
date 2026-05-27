using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DTO.Postulaciones;
using PortalTrabajo.Utility.Interfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
namespace PortalTrabajo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class PostulacionController : ControllerBase
    {
        private readonly IPostulacionService _postulacionService;
        private readonly IPerfilEstudianteService _perfilEstudianteService;
        private readonly ICvGeneratorService _cvGeneratorService;
        public PostulacionController(
            IPostulacionService postulacionService,
            IPerfilEstudianteService perfilEstudianteService,
            ICvGeneratorService cvGeneratorService)
        {
            _postulacionService = postulacionService;
            _perfilEstudianteService = perfilEstudianteService;
            _cvGeneratorService = cvGeneratorService;
        }
        [HttpPost("Aplicar")]
        public async Task<IActionResult> Aplicar([FromBody] CreatePostulacionDTO dto)
        {
            try
            {
                var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(claimId, out int usuarioId))
                    return Unauthorized(new { status = false, msg = "Token inválido." });
                var resultado = await _postulacionService.AplicarOfertaAsync(usuarioId, dto);
                if (resultado)
                    return Ok(new { status = true, msg = "Postulación enviada exitosamente." });
                else
                    return BadRequest(new { status = false, msg = "No se pudo procesar la postulación." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, msg = ex.Message });
            }
        }
        [HttpGet("MisPostulaciones")]
        public async Task<IActionResult> MisPostulaciones()
        {
            try
            {
                var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(claimId, out int usuarioId))
                    return Unauthorized(new { status = false, msg = "Token inválido." });
                var postulaciones = await _postulacionService.ObtenerMisPostulacionesAsync(usuarioId);
                return Ok(new { status = true, value = postulaciones });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, msg = ex.Message });
            }
        }
        [HttpGet("Oferta/{ofertaId}")]
        [Authorize(Roles = "Empresa")]
        public async Task<IActionResult> GetPostulacionesPorOferta(int ofertaId)
        {
            try
            {
                var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(claimId, out int usuarioEmpresaId))
                    return Unauthorized(new { status = false, msg = "Token inválido." });
                var postulaciones = await _postulacionService.ObtenerPostulacionesPorOfertaAsync(ofertaId, usuarioEmpresaId);
                return Ok(new { status = true, value = postulaciones });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, msg = "Ocurrió un error al obtener los candidatos: " + ex.Message });
            }
        }
        [HttpGet("CV/{perfilId}")]
        [Authorize(Roles = "Empresa")]
        public async Task<IActionResult> DescargarCVEstudiante(int perfilId)
        {
            try
            {
                var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(claimId, out int usuarioEmpresaId))
                    return Unauthorized(new { status = false, msg = "Token inválido." });
                var perfilCompleto = await _perfilEstudianteService.GetPerfilByPerfilIdAsync(perfilId);
                var pdfBytes = await _cvGeneratorService.GenerarCvUnivoAsync(perfilCompleto);
                return File(pdfBytes, "application/pdf", $"CV_{perfilCompleto.Nombres}_{perfilCompleto.Apellidos}.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, msg = "Ocurrió un error al generar el CV del estudiante: " + ex.Message });
            }
        }
    }
}
