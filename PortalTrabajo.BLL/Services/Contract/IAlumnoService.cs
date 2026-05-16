using PortalTrabajo.DTO.Alumnos;
using PortalTrabajo.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PortalTrabajo.BLL.Services.Contract
{
    public interface IAlumnoService
    {
        Task<AlumnoActivoDTO> Consultar(VerificarAlumnoDTO model);

        Task<SessionDTO> Registrar(RegistroEstudianteDTO model);
    }
}
