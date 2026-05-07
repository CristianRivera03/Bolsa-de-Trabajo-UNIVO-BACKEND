using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalTrabajo.API.Utility;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DTO.OfertasLaborales;
using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<OfertaLaboralDTO>>();
            try
            {
                var lista = await _ofertaService.ObtenerTodos();
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
        public async Task<IActionResult> Crear([FromBody] OfertaLaboralCreateDTO model)
        {
            var rsp = new Response<OfertaLaboralDTO>();
            try
            {
                var ofertaCreada = await _ofertaService.Crear(model);

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

    }
}
