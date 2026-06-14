using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalTrabajo.BLL.Services.Contract;
using System;
using System.Threading.Tasks;

namespace PortalTrabajo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")] // Solo los administradores pueden descargar reportes
    public class ReportesController : ControllerBase
    {
        private readonly IReportesService _reportesService;

        public ReportesController(IReportesService reportesService)
        {
            _reportesService = reportesService;
        }

        [HttpGet("Candidatos")]
        public async Task<IActionResult> GenerarReporteCandidatos(
            [FromQuery] DateTime? fechaInicio, 
            [FromQuery] DateTime? fechaFin,
            [FromQuery] int? carreraId, 
            [FromQuery] int? gradoAcademicoId,
            [FromQuery] bool? estado, 
            [FromQuery] int? departamentoId)
        {
            try
            {
                var fileBytes = await _reportesService.GenerarReporteCandidatosExcelAsync(fechaInicio, fechaFin, carreraId, gradoAcademicoId, estado, departamentoId);
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"ReporteCandidatos_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, msg = "Error al generar reporte de candidatos: " + ex.Message });
            }
        }

        [HttpGet("Empresas")]
        public async Task<IActionResult> GenerarReporteEmpresas(
            [FromQuery] DateTime? fechaInicio, 
            [FromQuery] DateTime? fechaFin,
            [FromQuery] int? sectorId)
        {
            try
            {
                var fileBytes = await _reportesService.GenerarReporteEmpresasExcelAsync(fechaInicio, fechaFin, sectorId);
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"ReporteEmpresas_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, msg = "Error al generar reporte de empresas: " + ex.Message });
            }
        }
    }
}
