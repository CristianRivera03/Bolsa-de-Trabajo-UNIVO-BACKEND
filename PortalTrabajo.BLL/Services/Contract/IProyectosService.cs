using PortalTrabajo.DTO.PerfilesEstudiante;
using System;
using System.Collections.Generic;
using System.Text;
namespace PortalTrabajo.BLL.Services.Contract
{
    public interface IProyectosService
    {
        Task<bool> AddProyectoAsync(int usuarioId, ProyectoEstudianteDTO dto);
        Task<bool> DeleteProyectoAsync(int usuarioId, int idProyecto);
        Task<bool> UpdateProyectoAsync(int usuarioId, int idProyecto, ProyectoEstudianteDTO dto);
    }
}
