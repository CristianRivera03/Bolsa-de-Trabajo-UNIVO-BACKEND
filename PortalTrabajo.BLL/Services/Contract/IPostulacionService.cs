using PortalTrabajo.DTO.Postulaciones;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTrabajo.BLL.Services.Contract;

public interface IPostulacionService
{
    Task<PostulacionDTO> PostularseAsync(int usuarioId, CreatePostulacionDTO model);
    Task<IEnumerable<PostulacionDTO>> GetPostulacionesPorUsuarioAsync(int usuarioId);
}
