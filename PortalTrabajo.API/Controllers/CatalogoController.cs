using Microsoft.AspNetCore.Mvc;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DTO.Catalogos;
using PortalTrabajo.API.Utility;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace PortalTrabajo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private readonly ICatalogoService _catalogoService;

        public CatalogoController(ICatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        [HttpGet("carreras")]
        public async Task<IActionResult> ObtenerCarreras()
        {
            try
            {
                var response = await _catalogoService.ObtenerCarreras();
                return Ok(new Response<List<CatalogDTO>> { status = true, value = response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<List<CatalogDTO>> { status = false, msg = ex.Message });
            }
        }

        [HttpGet("modalidades")]
        public async Task<IActionResult> ObtenerModalidades()
        {
            try
            {
                var response = await _catalogoService.ObtenerModalidades();
                return Ok(new Response<List<CatalogDTO>> { status = true, value = response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<List<CatalogDTO>> { status = false, msg = ex.Message });
            }
        }

        [HttpGet("niveles-idioma")]
        public async Task<IActionResult> ObtenerNivelesIdioma()
        {
            try
            {
                var response = await _catalogoService.ObtenerNivelesIdioma();
                return Ok(new Response<List<CatalogDTO>> { status = true, value = response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<List<CatalogDTO>> { status = false, msg = ex.Message });
            }
        }

        [HttpGet("grados-academicos")]
        public async Task<IActionResult> ObtenerGradosAcademicos()
        {
            try
            {
                var response = await _catalogoService.ObtenerGradosAcademicos();
                return Ok(new Response<List<CatalogDTO>> { status = true, value = response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<List<CatalogDTO>> { status = false, msg = ex.Message });
            }
        }

        [HttpGet("estados-postulacion")]
        public async Task<IActionResult> ObtenerEstadosPostulacion()
        {
            try
            {
                var response = await _catalogoService.ObtenerEstadosPostulacion();
                return Ok(new Response<List<CatalogDTO>> { status = true, value = response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<List<CatalogDTO>> { status = false, msg = ex.Message });
            }
        }
    }
}
