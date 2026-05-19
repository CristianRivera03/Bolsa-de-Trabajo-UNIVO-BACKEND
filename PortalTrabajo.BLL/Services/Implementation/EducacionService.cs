using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DAL.Repositories.Contract;
using PortalTrabajo.DTO.PerfilesEstudiante;
using PortalTrabajo.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PortalTrabajo.BLL.Services.Implementation
{
    public class EducacionService : IEducacionService
    {
        private readonly IGenericRepository<Educacion> _educacionRepo;
        private readonly IGenericRepository<PerfilesEstudiante> _perfilRepo;

        private readonly string[] EstadosPermitidos = { "En curso", "Pausado", "Egresado", "Graduado" };

        public EducacionService(
            IGenericRepository<Educacion> educacionRepo,
            IGenericRepository<PerfilesEstudiante> perfilRepo)
        {
            _educacionRepo = educacionRepo;
            _perfilRepo = perfilRepo;
        }

        private void ValidarEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado) || !EstadosPermitidos.Contains(estado))
            {
                throw new Exception($"Estado inválido. Los valores permitidos son: {string.Join(", ", EstadosPermitidos)}.");
            }
        }

        public async Task<bool> AddEducacionAsync(int usuarioId, EducacionDTO dto)
        {
            ValidarEstado(dto.Estado); 

            var perfil = await _perfilRepo.Get(p => p.UsuarioId == usuarioId);
            if (perfil == null) throw new Exception("No se encontró el perfil del estudiante.");

            var nuevaEducacion = new Educacion
            {
                PerfilId = perfil.Id,
                GradoAcademicoId = dto.GradoAcademicoId,
                Institucion = dto.Institucion,
                TituloObtenido = dto.TituloObtenido,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                Estado = dto.Estado 
            };

            var educacionCreada = await _educacionRepo.Create(nuevaEducacion);
            return educacionCreada != null && educacionCreada.Id > 0;
        }

        public async Task<bool> UpdateEducacionAsync(int usuarioId, int idEducacion, EducacionDTO dto)
        {
            ValidarEstado(dto.Estado); 

            var perfil = await _perfilRepo.Get(p => p.UsuarioId == usuarioId);
            if (perfil == null) throw new Exception("No se encontró el perfil del estudiante.");

            var educacion = await _educacionRepo.Get(e => e.Id == idEducacion && e.PerfilId == perfil.Id);
            if (educacion == null) throw new Exception("El registro de educación no existe o no te pertenece.");

            educacion.GradoAcademicoId = dto.GradoAcademicoId;
            educacion.Institucion = dto.Institucion;
            educacion.TituloObtenido = dto.TituloObtenido;
            educacion.FechaInicio = dto.FechaInicio;
            educacion.FechaFin = dto.FechaFin;
            educacion.Estado = dto.Estado;

            return await _educacionRepo.Update(educacion);
        }

        public async Task<bool> DeleteEducacionAsync(int usuarioId, int idEducacion)
        {
            var perfil = await _perfilRepo.Get(p => p.UsuarioId == usuarioId);
            if (perfil == null) return false;

            var educacion = await _educacionRepo.Get(e => e.Id == idEducacion && e.PerfilId == perfil.Id);
            if (educacion == null) throw new Exception("El registro de educación no existe o no tienes permisos.");

            return await _educacionRepo.HardDelete(educacion);
        }
    }
}