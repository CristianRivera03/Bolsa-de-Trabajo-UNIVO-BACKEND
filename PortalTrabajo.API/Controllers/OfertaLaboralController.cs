using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalTrabajo.API.Utility;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DTO.OfertasLaborales;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
namespace PortalTrabajo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfertaLaboralController : ControllerBase
    {
        private readonly IOfertaLaboralService _ofertaService;
        public OfertaLaboralController(IOfertaLaboralService ofertaService)
        {
            _ofertaService = ofertaService;
        }
        [HttpGet("lista")]
        public async Task<IActionResult> Lista([FromQuery] string keyword = null, [FromQuery] int? carreraId = null, [FromQuery] int? sectorId = null)
        {
            var rsp = new Response<List<OfertaLaboralDTO>>();
            try
            {
                var lista = await _ofertaService.ObtenerTodos(keyword, carreraId, sectorId);
                rsp.status = true;
                rsp.value = lista;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, rsp);
            }
        }
        [HttpGet("mis-ofertas")]
        [Authorize(Roles = "Empresa")]
        public async Task<IActionResult> MisOfertas()
        {
            var rsp = new Response<List<OfertaLaboralDTO>>();
            try
            {
                var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(claimId, out int usuarioEmpresaId))
                {
                    rsp.status = false;
                    rsp.msg = "Token inválido.";
                    return Unauthorized(rsp);
                }
                var lista = await _ofertaService.ObtenerMisOfertasAsync(usuarioEmpresaId);
                rsp.status = true;
                rsp.value = lista;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, rsp);
            }
        }
        [HttpPost("crear")]
        [Authorize(Roles = "Empresa")]
        public async Task<IActionResult> Crear([FromBody] OfertaLaboralCreateDTO model)
        {
            var rsp = new Response<OfertaLaboralDTO>();
            try
            {
                var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(claimId, out int usuarioEmpresaId))
                {
                    rsp.status = false;
                    rsp.msg = "Token inválido.";
                    return Unauthorized(rsp);
                }
                var ofertaCreada = await _ofertaService.Crear(model, usuarioEmpresaId);
                rsp.status = true;
                rsp.value = ofertaCreada;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, rsp);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var oferta = await _ofertaService.ObtenerPorId(id);
                if (oferta == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        value = (OfertaLaboralDTO)null,
                        msg = $"No se encontró ninguna oferta con el ID {id}"
                    });
                }
                return Ok(new
                {
                    status = true,
                    value = oferta,
                    msg = "Oferta obtenida con éxito"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = false,
                    value = (OfertaLaboralDTO)null,
                    msg = ex.Message
                });
            }
        }
    }
}
