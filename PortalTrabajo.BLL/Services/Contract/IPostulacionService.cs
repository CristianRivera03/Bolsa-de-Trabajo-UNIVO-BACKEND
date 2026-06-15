using PortalTrabajo.DTO.Postulaciones;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PortalTrabajo.BLL.Services.Contract;
public interface IPostulacionService
{
    Task<bool> AplicarOfertaAsync(int usuarioId, CreatePostulacionDTO dto);
    Task<IEnumerable<PostulacionDTO>> ObtenerMisPostulacionesAsync(int usuarioId);
    Task<IEnumerable<PostulacionDTO>> ObtenerPostulacionesPorOfertaAsync(int ofertaId, int usuarioEmpresaId);
    Task<bool> VerificarAccesoACvEstudianteAsync(int perfilId, int usuarioEmpresaId);
    Task<bool> CambiarEstadoPostulacionAsync(int postulacionId, int nuevoEstadoId, int usuarioEmpresaId);
}
