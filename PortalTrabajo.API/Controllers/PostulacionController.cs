using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalTrabajo.API.Utility;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DTO.Postulaciones;
using PortalTrabajo.Utility;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PortalTrabajo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize(Roles = "Estudiante,Alumno")]
public class PostulacionController : ControllerBase
{
    private readonly IPostulacionService _postulacionService;

    public PostulacionController(IPostulacionService postulacionService)
    {
        _postulacionService = postulacionService;
    }

    [HttpPost]
    public async Task<IActionResult> Postularse([FromBody] CreatePostulacionDTO dto)
    {
        try
        {
            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                usuarioId = 1; // Id de prueba
            }

            var result = await _postulacionService.PostularseAsync(usuarioId, dto);
            return Ok(new Response<PostulacionDTO> { status = true, value = result });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<PostulacionDTO> { status = false, msg = ex.Message });
        }
    }

    [HttpGet("MisPostulaciones")]
    public async Task<IActionResult> GetMisPostulaciones()
    {
        try
        {
            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                usuarioId = 1; // Id de prueba
            }

            var result = await _postulacionService.GetPostulacionesPorUsuarioAsync(usuarioId);
            return Ok(new Response<IEnumerable<PostulacionDTO>> { status = true, value = result });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<IEnumerable<PostulacionDTO>> { status = false, msg = ex.Message });
        }
    }
}
