using PortalTrabajo.DTO.Empresas;
using System.Threading.Tasks;
namespace PortalTrabajo.BLL.Services.Contract
{
    public interface IEmpresaService
    {
        Task<EmpresaDTO> CrearEmpresa(EmpresaCreateDTO modelo);
        Task<EmpresaDTO> ObtenerMiEmpresaAsync(int usuarioId);
        Task<EmpresaDTO> ActualizarEmpresaAsync(int usuarioId, EmpresaUpdateDTO dto);
        Task<EmpresaDTO> ObtenerEmpresaPorIdAsync(int empresaId);
        Task<string> CambiarLogoAsync(int usuarioId, PortalTrabajo.DTO.Shared.CambiarImagenDTO dto);
    }
}
