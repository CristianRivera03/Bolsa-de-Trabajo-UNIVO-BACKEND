using System;
using System.Collections.Generic;
using PortalTrabajo.DTO.PerfilesEstudiante;
namespace PortalTrabajo.DTO.Postulaciones
{
    public class PostulacionDTO
    {
        public int Id { get; set; }
        public int OfertaId { get; set; }
        public string OfertaTitulo { get; set; }
        public string EmpresaNombre { get; set; }
        public int PerfilId { get; set; }
        public string EstudianteNombreCompleto { get; set; }
        public DateTime FechaPostulacion { get; set; }
        public string EstadoNombre { get; set; }
        public string Mensaje { get; set; }
        public string? EstudianteFotoUrl { get; set; }
        public string? EmpresaLogoUrl { get; set; }
        public List<EstudianteHabilidadDTO> EstudianteHabilidades { get; set; } = new List<EstudianteHabilidadDTO>();
    }
    public class CreatePostulacionDTO
    {
        public int OfertaId { get; set; }
        public string Mensaje { get; set; }
    }
}
