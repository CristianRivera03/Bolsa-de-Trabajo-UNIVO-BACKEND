using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalTrabajo.API.Utility;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DTO.PerfilesEstudiante;
using PortalTrabajo.Utility;
using PortalTrabajo.Utility.Interfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PortalTrabajo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
    [Authorize(Roles = "Estudiante,Alumno")] // Descomentar cuando la auth esté lista
public class PerfilEstudianteController : ControllerBase
{
    private readonly IPerfilEstudianteService _perfilService;
    private readonly ICvGeneratorService _cvGeneratorService;

    public PerfilEstudianteController(IPerfilEstudianteService perfilService, ICvGeneratorService cvGeneratorService)
    {
        _perfilService = perfilService;
        _cvGeneratorService = cvGeneratorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMiPerfil()
    {
        try
        {
            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                return Unauthorized("Token inválido o expirado");
            }

            var perfil = await _perfilService.GetPerfilByUsuarioIdAsync(usuarioId);
            return Ok(new Response<PerfilEstudianteDTO> { status = true, value = perfil });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<PerfilEstudianteDTO> { status = false, msg = ex.Message });
        }
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateMiPerfil([FromBody] PerfilEstudianteUpdateDTO dto)
    {
        try
        {
            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                return Unauthorized("Token inválido o expirado");
            }

            var perfilActualizado = await _perfilService.UpdatePerfilAsync(usuarioId, dto);
            return Ok(new Response<PerfilEstudianteDTO> { status = true, value = perfilActualizado });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<PerfilEstudianteDTO> { status = false, msg = ex.Message });
        }
    }

    [HttpPost("CambiarFoto")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CambiarFoto(IFormFile Archivo) 
    {
        try
        {
            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                return Unauthorized("Token inválido o expirado");
            }

            if (Archivo == null || Archivo.Length == 0)
            {
                return BadRequest(new Response<string> { status = false, msg = "No se ha enviado ninguna imagen." });
            }

            var dto = new PortalTrabajo.DTO.Shared.CambiarImagenDTO
            {
                Archivo = Archivo
            };

            var nuevaUrl = await _perfilService.CambiarFotoAsync(usuarioId, dto);
            return Ok(new Response<string> { status = true, value = nuevaUrl });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<string> { status = false, msg = ex.Message });
        }
    }

    [HttpGet("GenerarCV")]
    public async Task<IActionResult> DescargarCV()
    {
        try
        {
            var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(claimId, out int usuarioId))
                return Unauthorized("Token inválido.");

            var perfilCompleto = await _perfilService.GetPerfilByUsuarioIdAsync(usuarioId);

            // AHORA ES AWAIT
            var pdfBytes = await _cvGeneratorService.GenerarCvUnivoAsync(perfilCompleto);

            return File(pdfBytes, "application/pdf", $"CV_{perfilCompleto.Nombres}.pdf");
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, new { status = false, msg = ex.Message });
        }
    }

}
