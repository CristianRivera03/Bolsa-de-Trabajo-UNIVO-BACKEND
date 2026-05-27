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
        Task<List<CatalogDTO>> ObtenerDepartamentos();
        Task<List<CatalogDTO>> ObtenerMunicipios(int departamentoId);
        Task<List<CatalogDTO>> ObtenerDistritos(int municipioId);
        Task<List<CatalogDTO>> ObtenerTiposContrato();
        Task<List<CatalogDTO>> ObtenerTiposLicencia();
        Task<List<CatalogDTO>> ObtenerGeneros();
        Task<List<CatalogDTO>> ObtenerHabilidades();
    }
}
