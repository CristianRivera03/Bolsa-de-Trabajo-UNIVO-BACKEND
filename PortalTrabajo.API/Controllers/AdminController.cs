using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalTrabajo.API.Utility;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DTO.Admin;
using PortalTrabajo.DTO.Usuarios;
using PortalTrabajo.DTO.OfertasLaborales;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PortalTrabajo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var rsp = new Response<AdminDashboardStatsDTO>();
            try
            {
                var stats = await _adminService.GetDashboardStatsAsync();
                rsp.status = true;
                rsp.value = stats;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, rsp);
            }
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var rsp = new Response<IEnumerable<UsuarioDTO>>();
            try
            {
                var users = await _adminService.GetUsersAsync();
                rsp.status = true;
                rsp.value = users;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, rsp);
            }
        }
        [HttpPost("users/{id:int}/toggle")]
        public async Task<IActionResult> ToggleUser(int id, [FromQuery] bool active)
        {
            var rsp = new Response<bool>();
            try
            {
                var success = await _adminService.ToggleUserStatusAsync(id, active);
                if (success)
                {
                    rsp.status = true;
                    rsp.value = true;
                    return Ok(rsp);
                }
                rsp.status = false;
                rsp.msg = "No se pudo actualizar el estado del usuario.";
                return BadRequest(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, rsp);
            }
        }
        [HttpGet("companies")]
        public async Task<IActionResult> GetCompanies()
        {
            var rsp = new Response<IEnumerable<AdminEmpresaDTO>>();
            try
            {
                var companies = await _adminService.GetCompaniesAsync();
                rsp.status = true;
                rsp.value = companies;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, rsp);
            }
        }
        [HttpPost("companies/{id:int}/toggle")]
        public async Task<IActionResult> ToggleCompany(int id, [FromQuery] bool active)
        {
            var rsp = new Response<bool>();
            try
            {
                var success = await _adminService.ToggleCompanyStatusAsync(id, active);
                if (success)
                {
                    rsp.status = true;
                    rsp.value = true;
                    return Ok(rsp);
                }
                rsp.status = false;
                rsp.msg = "No se pudo actualizar el estado de la empresa.";
                return BadRequest(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, rsp);
            }
        }
        [HttpGet("jobposts")]
        public async Task<IActionResult> GetJobPosts()
        {
            var rsp = new Response<IEnumerable<OfertaLaboralDTO>>();
            try
            {
                var posts = await _adminService.GetJobPostsAsync();
                rsp.status = true;
                rsp.value = posts;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, rsp);
            }
        }
        [HttpPost("jobposts/{id:int}/toggle")]
        public async Task<IActionResult> ToggleJobPost(int id, [FromQuery] bool active)
        {
            var rsp = new Response<bool>();
            try
            {
                var success = await _adminService.ToggleJobPostStatusAsync(id, active);
                if (success)
                {
                    rsp.status = true;
                    rsp.value = true;
                    return Ok(rsp);
                }
                rsp.status = false;
                rsp.msg = "No se pudo actualizar el estado de la publicación.";
                return BadRequest(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, rsp);
            }
        }

        [HttpGet("audit-logs")]
        public async Task<IActionResult> GetAuditLogs([FromQuery] AuditLogFilterDTO filter)
        {
            var rsp = new Response<PaginatedResponse<AuditLogDTO>>();
            try
            {
                var logs = await _adminService.GetAuditLogsAsync(filter);
                rsp.status = true;
                rsp.value = logs;
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
