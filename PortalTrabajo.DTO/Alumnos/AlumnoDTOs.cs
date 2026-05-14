using System;

namespace PortalTrabajo.DTO.Alumnos
{
    // 1. DTO para consultar si existe en el mock de la universidad
    public class VerificarAlumnoDTO
    {
        public string Carnet { get; set; }
        public string PasswordPortal { get; set; }
    }

    // 2. DTO que devuelve la info encontrada para autocompletar el formulario
    public class AlumnoActivoDTO
    {
        public string Carnet { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Genero { get; set; }
        public DateOnly FechaNacimiento { get; set; }
    }

    // 3. DTO para enviar el formulario final y registrarse en el sistema
    public class RegistroEstudianteDTO
    {
        public string Carnet { get; set; }
        public string PasswordPortal { get; set; }
    }
}
