using Microsoft.AspNetCore.Mvc;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DTO.Catalogos;
using PortalTrabajo.API.Utility;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using PortalTrabajo.DAL.DBContext;

namespace PortalTrabajo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private readonly ICatalogoService _catalogoService;
        private readonly PortalTrabajoDbContext _context;

        public CatalogoController(ICatalogoService catalogoService, PortalTrabajoDbContext context)
        {
            _catalogoService = catalogoService;
            _context = context;
        }

        [HttpGet("carreras")]
        public async Task<IActionResult> ObtenerCarreras()
        {
            try { return Ok(new Response<List<CatalogDTO>> { status = true, value = await _catalogoService.ObtenerCarreras() }); }
            catch (Exception ex) { return StatusCode(500, new Response<List<CatalogDTO>> { status = false, msg = ex.Message }); }
        }

        [HttpGet("modalidades")]
        public async Task<IActionResult> ObtenerModalidades()
        {
            try { return Ok(new Response<List<CatalogDTO>> { status = true, value = await _catalogoService.ObtenerModalidades() }); }
            catch (Exception ex) { return StatusCode(500, new Response<List<CatalogDTO>> { status = false, msg = ex.Message }); }
        }

        [HttpGet("niveles-idioma")]
        public async Task<IActionResult> ObtenerNivelesIdioma()
        {
            try { return Ok(new Response<List<CatalogDTO>> { status = true, value = await _catalogoService.ObtenerNivelesIdioma() }); }
            catch (Exception ex) { return StatusCode(500, new Response<List<CatalogDTO>> { status = false, msg = ex.Message }); }
        }

        [HttpGet("grados-academicos")]
        public async Task<IActionResult> ObtenerGradosAcademicos()
        {
            try { return Ok(new Response<List<CatalogDTO>> { status = true, value = await _catalogoService.ObtenerGradosAcademicos() }); }
            catch (Exception ex) { return StatusCode(500, new Response<List<CatalogDTO>> { status = false, msg = ex.Message }); }
        }

        [HttpGet("estados-postulacion")]
        public async Task<IActionResult> ObtenerEstadosPostulacion()
        {
            try { return Ok(new Response<List<CatalogDTO>> { status = true, value = await _catalogoService.ObtenerEstadosPostulacion() }); }
            catch (Exception ex) { return StatusCode(500, new Response<List<CatalogDTO>> { status = false, msg = ex.Message }); }
        }

        [HttpGet("departamentos")]
        public async Task<IActionResult> ObtenerDepartamentos()
        {
            try { return Ok(new Response<List<CatalogDTO>> { status = true, value = await _catalogoService.ObtenerDepartamentos() }); }
            catch (Exception ex) { return StatusCode(500, new Response<List<CatalogDTO>> { status = false, msg = ex.Message }); }
        }

        [HttpGet("municipios/{departamentoId}")]
        public async Task<IActionResult> ObtenerMunicipios(int departamentoId)
        {
            try { return Ok(new Response<List<CatalogDTO>> { status = true, value = await _catalogoService.ObtenerMunicipios(departamentoId) }); }
            catch (Exception ex) { return StatusCode(500, new Response<List<CatalogDTO>> { status = false, msg = ex.Message }); }
        }

        [HttpGet("distritos/{municipioId}")]
        public async Task<IActionResult> ObtenerDistritos(int municipioId)
        {
            try { return Ok(new Response<List<CatalogDTO>> { status = true, value = await _catalogoService.ObtenerDistritos(municipioId) }); }
            catch (Exception ex) { return StatusCode(500, new Response<List<CatalogDTO>> { status = false, msg = ex.Message }); }
        }

        [HttpGet("tipos-contrato")]
        public async Task<IActionResult> ObtenerTiposContrato()
        {
            try { return Ok(new Response<List<CatalogDTO>> { status = true, value = await _catalogoService.ObtenerTiposContrato() }); }
            catch (Exception ex) { return StatusCode(500, new Response<List<CatalogDTO>> { status = false, msg = ex.Message }); }
        }

        [HttpGet("tipos-licencia")]
        public async Task<IActionResult> ObtenerTiposLicencia()
        {
            try { return Ok(new Response<List<CatalogDTO>> { status = true, value = await _catalogoService.ObtenerTiposLicencia() }); }
            catch (Exception ex) { return StatusCode(500, new Response<List<CatalogDTO>> { status = false, msg = ex.Message }); }
        }

        [HttpGet("generos")]
        public async Task<IActionResult> ObtenerGeneros()
        {
            try { return Ok(new Response<List<CatalogDTO>> { status = true, value = await _catalogoService.ObtenerGeneros() }); }
            catch (Exception ex) { return StatusCode(500, new Response<List<CatalogDTO>> { status = false, msg = ex.Message }); }
        }

        [HttpGet("habilidades")]
        public async Task<IActionResult> ObtenerHabilidades()
        {
            try { return Ok(new Response<List<CatalogDTO>> { status = true, value = await _catalogoService.ObtenerHabilidades() }); }
            catch (Exception ex) { return StatusCode(500, new Response<List<CatalogDTO>> { status = false, msg = ex.Message }); }
        }

        [HttpGet("sectores")]
        public async Task<IActionResult> ObtenerSectores()
        {
            try { return Ok(new Response<List<CatalogDTO>> { status = true, value = await _catalogoService.ObtenerSectores() }); }
            catch (Exception ex) { return StatusCode(500, new Response<List<CatalogDTO>> { status = false, msg = ex.Message }); }
        }

        // CRUD ADMIN
        [HttpPost("habilidades")]
        public async Task<IActionResult> CrearHabilidad([FromBody] CatalogDTO dto)
        {
            try { return Ok(new Response<CatalogDTO> { status = true, value = await _catalogoService.CrearHabilidad(dto) }); }
            catch (Exception ex) { return StatusCode(500, new Response<CatalogDTO> { status = false, msg = ex.Message }); }
        }

        [HttpDelete("habilidades/{id}")]
        public async Task<IActionResult> EliminarHabilidad(int id)
        {
            try { var result = await _catalogoService.EliminarHabilidad(id); return Ok(new Response<bool> { status = result, value = result }); }
            catch (Exception ex) { return StatusCode(500, new Response<bool> { status = false, msg = ex.Message }); }
        }

        [HttpPost("sectores")]
        public async Task<IActionResult> CrearSector([FromBody] CatalogDTO dto)
        {
            try { return Ok(new Response<CatalogDTO> { status = true, value = await _catalogoService.CrearSector(dto) }); }
            catch (Exception ex) { return StatusCode(500, new Response<CatalogDTO> { status = false, msg = ex.Message }); }
        }

        [HttpDelete("sectores/{id}")]
        public async Task<IActionResult> EliminarSector(int id)
        {
            try { var result = await _catalogoService.EliminarSector(id); return Ok(new Response<bool> { status = result, value = result }); }
            catch (Exception ex) { return StatusCode(500, new Response<bool> { status = false, msg = ex.Message }); }
        }

        [HttpPost("carreras")]
        public async Task<IActionResult> CrearCarrera([FromBody] CatalogDTO dto)
        {
            try { return Ok(new Response<CatalogDTO> { status = true, value = await _catalogoService.CrearCarrera(dto) }); }
            catch (Exception ex) { return StatusCode(500, new Response<CatalogDTO> { status = false, msg = ex.Message }); }
        }

        [HttpDelete("carreras/{id}")]
        public async Task<IActionResult> EliminarCarrera(int id)
        {
            try { var result = await _catalogoService.EliminarCarrera(id); return Ok(new Response<bool> { status = result, value = result }); }
            catch (Exception ex) { return StatusCode(500, new Response<bool> { status = false, msg = ex.Message }); }
        }

        [HttpGet("SetupDB")]
        public async Task<IActionResult> SetupDB()
        {
            try
            {
                var sql = @"
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CatSectores' and xtype='U')
BEGIN
    CREATE TABLE CatSectores (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Nombre NVARCHAR(150) NOT NULL
    );
END

IF EXISTS (SELECT * FROM sys.columns WHERE Name = N'Sector' AND Object_ID = Object_ID(N'Empresas'))
BEGIN
    ALTER TABLE Empresas DROP COLUMN Sector;
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'SectorId' AND Object_ID = Object_ID(N'Empresas'))
BEGIN
    ALTER TABLE Empresas ADD SectorId INT NULL;
    ALTER TABLE Empresas ADD CONSTRAINT FK_Empresas_Sector FOREIGN KEY (SectorId) REFERENCES CatSectores(Id);
END
";
                await _context.Database.ExecuteSqlRawAsync(sql);
                return Ok("Setup executed successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
