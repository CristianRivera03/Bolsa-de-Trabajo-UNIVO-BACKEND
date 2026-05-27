using System;
namespace PortalTrabajo.DTO.Alumnos
{
    public class VerificarAlumnoDTO
    {
        public string Carnet { get; set; }
        public string PasswordPortal { get; set; }
    }
    public class AlumnoActivoDTO
    {
        public string Carnet { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Genero { get; set; }
        public DateOnly FechaNacimiento { get; set; }
    }
    public class RegistroEstudianteDTO
    {
        public string Carnet { get; set; }
        public string PasswordPortal { get; set; }
    }
}
