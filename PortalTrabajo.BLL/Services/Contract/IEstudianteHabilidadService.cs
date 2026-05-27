using PortalTrabajo.DTO.PerfilesEstudiante;
using System;
using System.Collections.Generic;
using System.Text;
namespace PortalTrabajo.BLL.Services.Contract
{
    public interface IEstudianteHabilidadService
    {
        Task<bool> AddHabilidadAsync(int usuarioId, EstudianteHabilidadDTO dto);
        Task<bool> UpdateHabilidadAsync(int usuarioId, int habilidadId, EstudianteHabilidadDTO dto);
        Task<bool> DeleteHabilidadAsync(int usuarioId, int habilidadId);
    }
}
