using PortalTrabajo.DTO.PerfilesEstudiante;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortalTrabajo.BLL.Services.Contract
{
    public interface IExperienciaLaboralService
    {
        Task<bool> AddExperienciaAsync(int usuarioId, ExperienciaLaboralDTO dto);
        Task<bool> DeleteExperienciaAsync(int usuarioId, int idExperiencia);
        Task<bool> UpdateExperienciaAsync(int usuarioId, int idExperiencia, ExperienciaLaboralDTO dto);

    }
}
