using PortalTrabajo.DTO.Empresas;
using System.Threading.Tasks;

namespace PortalTrabajo.BLL.Services.Contract
{
    public interface IEmpresaService
    {
        Task<EmpresaDTO> CrearEmpresa(EmpresaCreateDTO modelo);
    }
}
