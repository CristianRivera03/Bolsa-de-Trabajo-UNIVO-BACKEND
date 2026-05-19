using PortalTrabajo.DTO.PerfilesEstudiante;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortalTrabajo.BLL.Services.Contract
{
    public interface IEstudianteIdiomaService
    {
        Task<bool> AddIdiomaAsync(int usuarioId, EstudianteIdiomaDTO dto);
        Task<bool> UpdateIdiomaAsync(int usuarioId, int idIdioma, EstudianteIdiomaDTO dto);
        Task<bool> DeleteIdiomaAsync(int usuarioId, int idIdioma);
    }
}
