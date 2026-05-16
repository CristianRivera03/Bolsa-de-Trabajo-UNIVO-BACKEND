using PortalTrabajo.DTO.OfertasLaborales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTrabajo.BLL.Services.Contract
{
    public interface IOfertaLaboralService
    {
        Task<List<OfertaLaboralDTO>> ObtenerTodos();
        Task<OfertaLaboralDTO> Crear(OfertaLaboralCreateDTO modelo);
        Task<OfertaLaboralDTO> ObtenerPorId(int id);
        Task<bool> DesactivarOfertasExpiradas();
    }
}
