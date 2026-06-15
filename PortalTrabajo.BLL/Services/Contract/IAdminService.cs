using PortalTrabajo.DTO.Admin;
using PortalTrabajo.DTO.Usuarios;
using PortalTrabajo.DTO.OfertasLaborales;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PortalTrabajo.BLL.Services.Contract
{
    public interface IAdminService
    {
        Task<AdminDashboardStatsDTO> GetDashboardStatsAsync();
        Task<IEnumerable<UsuarioDTO>> GetUsersAsync();
        Task<bool> ToggleUserStatusAsync(int userId, bool active);
        Task<IEnumerable<AdminEmpresaDTO>> GetCompaniesAsync();
        Task<bool> ToggleCompanyStatusAsync(int companyId, bool active);
        Task<IEnumerable<OfertaLaboralDTO>> GetJobPostsAsync();
        Task<bool> ToggleJobPostStatusAsync(int jobId, bool active);

        Task<PaginatedResponse<AuditLogDTO>> GetAuditLogsAsync(AuditLogFilterDTO filter);
    }
}
