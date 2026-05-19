using PortalTrabajo.DTO.PerfilesEstudiante;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortalTrabajo.Utility.Interfaces
{
    public interface ICvGeneratorService
    {
        byte[] GenerarCvBasico(PerfilEstudianteDTO perfil);
    }
}
