using PortalTrabajo.DTO.PerfilesEstudiante;
using System;
using System.Collections.Generic;
using System.Text;
namespace PortalTrabajo.BLL.Services.Contract
{
    public interface IEducacionService
    {
        Task<bool> AddEducacionAsync(int usuarioId, EducacionDTO dto);
        Task<bool> UpdateEducacionAsync(int usuarioId, int idEducacion, EducacionDTO dto);
        Task<bool> DeleteEducacionAsync(int usuarioId, int idEducacion);
    }
}
