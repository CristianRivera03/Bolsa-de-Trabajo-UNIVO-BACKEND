using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalTrabajo.API.Utility;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DTO.PerfilesEstudiante;
using PortalTrabajo.Utility;
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

    public PerfilEstudianteController(IPerfilEstudianteService perfilService)
    {
        _perfilService = perfilService;
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

    [HttpPut]
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
    public async Task<IActionResult> CambiarFoto([FromForm] PortalTrabajo.DTO.Shared.CambiarImagenDTO dto)
    {
        try
        {
            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                return Unauthorized("Token inválido o expirado");
            }

            var nuevaUrl = await _perfilService.CambiarFotoAsync(usuarioId, dto);
            return Ok(new Response<string> { status = true, value = nuevaUrl });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<string> { status = false, msg = ex.Message });
        }
    }
}
