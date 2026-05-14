using PortalTrabajo.DTO.Catalogos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTrabajo.BLL.Services.Contract
{
    public interface ICatalogoService
    {
        Task<List<CatalogDTO>> ObtenerCarreras();
        Task<List<CatalogDTO>> ObtenerModalidades();
        Task<List<CatalogDTO>> ObtenerNivelesIdioma();
        Task<List<CatalogDTO>> ObtenerGradosAcademicos();
        Task<List<CatalogDTO>> ObtenerRoles();
        Task<List<CatalogDTO>> ObtenerEstadosPostulacion();
    }
}
