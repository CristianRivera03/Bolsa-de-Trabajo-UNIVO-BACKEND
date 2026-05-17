using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalTrabajo.API.Utility;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DTO.Empresas;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PortalTrabajo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpPost("registrar")]

        public async Task<IActionResult> Registrar([FromBody] EmpresaCreateDTO model)
        {
            var rsp = new Response<EmpresaDTO>();
            try
            {
                var empresa = await _empresaService.CrearEmpresa(model);
                rsp.status = true;
                rsp.value = empresa;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, rsp);
            }
        }

        [HttpGet("MiPerfil")]
        [Authorize(Roles = "Empresa")]
        public async Task<IActionResult> GetMiPerfil()
        {
            var rsp = new Response<EmpresaDTO>();
            try
            {
                var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out int usuarioId))
                {
                    return Unauthorized("Token inválido o expirado");
                }

                var empresa = await _empresaService.ObtenerMiEmpresaAsync(usuarioId);
                rsp.status = true;
                rsp.value = empresa;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(500, rsp);
            }
        }

        [HttpPut("MiPerfil")]
        [Authorize(Roles = "Empresa")]
        public async Task<IActionResult> UpdateMiPerfil([FromBody] EmpresaUpdateDTO dto)
        {
            var rsp = new Response<EmpresaDTO>();
            try
            {
                var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out int usuarioId))
                {
                    return Unauthorized("Token inválido o expirado");
                }

                var empresaActualizada = await _empresaService.ActualizarEmpresaAsync(usuarioId, dto);
                rsp.status = true;
                rsp.value = empresaActualizada;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(500, rsp);
            }
        }

        [HttpPost("CambiarLogo")]
        [Authorize(Roles = "Empresa")]
        public async Task<IActionResult> CambiarLogo([FromForm] PortalTrabajo.DTO.Shared.CambiarImagenDTO dto)
        {
            var rsp = new Response<string>();
            try
            {
                var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out int usuarioId))
                {
                    return Unauthorized("Token inválido o expirado");
                }

                var nuevaUrl = await _empresaService.CambiarLogoAsync(usuarioId, dto);
                rsp.status = true;
                rsp.value = nuevaUrl;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(500, rsp);
            }
        }
    }
}
