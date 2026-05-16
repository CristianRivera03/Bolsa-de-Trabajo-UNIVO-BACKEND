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
// [Authorize(Roles = "Estudiante,Alumno")] // Descomentar cuando la auth esté lista
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
                // TODO: Remover esto y retornar Unauthorized cuando JWT esté 100% integrado
                // Por ahora asumimos un Id para pruebas si no hay token
                usuarioId = 1; 
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
                usuarioId = 1; // Id de prueba si no hay token
            }

            var perfilActualizado = await _perfilService.UpdatePerfilAsync(usuarioId, dto);
            return Ok(new Response<PerfilEstudianteDTO> { status = true, value = perfilActualizado });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<PerfilEstudianteDTO> { status = false, msg = ex.Message });
        }
    }
}
