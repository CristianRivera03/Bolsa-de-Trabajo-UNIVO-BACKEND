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
        // Paso 1 y 2: Verifica credenciales en la DB de la universidad y retorna info
        Task<AlumnoActivoDTO> Consultar(VerificarAlumnoDTO model);

        // Paso 3: Registra al estudiante en la bolsa de trabajo y lo loguea automáticamente
        Task<SessionDTO> Registrar(RegistroEstudianteDTO model);
    }
}
