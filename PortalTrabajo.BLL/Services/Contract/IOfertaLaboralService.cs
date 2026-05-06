using PortalTrabajo.DTO.OfertasLaborales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTrabajo.BLL.Services.Contract
{
    public interface IOfertaLaboralService
    {
        Task<List<OfertaLaboralDTO>> ObtenerTodos();
    }
}
