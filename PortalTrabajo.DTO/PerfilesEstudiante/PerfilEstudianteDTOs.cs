using System;
using System.Collections.Generic;

namespace PortalTrabajo.DTO.PerfilesEstudiante
{
    public class PerfilEstudianteDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Carnet { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Genero { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string SobreMi { get; set; }
        public string FotoUrl { get; set; }
        public string EnlaceGitHub { get; set; }
        public string EnlaceLinkedIn { get; set; }

        public List<EducacionDTO> Educaciones { get; set; } = new List<EducacionDTO>();
        public List<ExperienciaLaboralDTO> ExperienciasLaborales { get; set; } = new List<ExperienciaLaboralDTO>();
        public List<EstudianteHabilidadDTO> Habilidades { get; set; } = new List<EstudianteHabilidadDTO>();
        public List<EstudianteIdiomaDTO> Idiomas { get; set; } = new List<EstudianteIdiomaDTO>();
    }

    public class EducacionDTO
    {
        public int Id { get; set; }
        public int GradoAcademicoId { get; set; }
        public string GradoAcademicoNombre { get; set; }
        public string Institucion { get; set; }
        public string TituloObtenido { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public string Estado { get; set; }
    }

    public class ExperienciaLaboralDTO
    {
        public int Id { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public bool? EsTrabajoActual { get; set; }
        public string Descripcion { get; set; }
    }

    public class EstudianteHabilidadDTO
    {
        public int HabilidadId { get; set; }
        public string NombreHabilidad { get; set; }
    }

    public class EstudianteIdiomaDTO
    {
        public int Id { get; set; }
        public string Idioma { get; set; }
        public int NivelId { get; set; }
        public string NivelNombre { get; set; }
    }

    public class PerfilEstudianteUpdateDTO
    {
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string SobreMi { get; set; }
        public string FotoUrl { get; set; }
        public string EnlaceGitHub { get; set; }
        public string EnlaceLinkedIn { get; set; }
    }
}
