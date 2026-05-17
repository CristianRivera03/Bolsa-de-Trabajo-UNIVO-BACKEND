using System.Threading.Tasks;
using PortalTrabajo.DTO.PerfilesEstudiante;

namespace PortalTrabajo.BLL.Services.Contract;

public interface IPerfilEstudianteService
{
    Task<PerfilEstudianteDTO> GetPerfilByUsuarioIdAsync(int usuarioId);
    Task<PerfilEstudianteDTO> UpdatePerfilAsync(int usuarioId, PerfilEstudianteUpdateDTO dto);
    Task<string> CambiarFotoAsync(int usuarioId, PortalTrabajo.DTO.Shared.CambiarImagenDTO dto);
}
