using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DAL.Repositories.Contract;
using PortalTrabajo.DTO.PerfilesEstudiante;
using PortalTrabajo.Model;
using System;
using System.Threading.Tasks;
namespace PortalTrabajo.BLL.Services.Implementation
{
    public class EstudianteHabilidadService : IEstudianteHabilidadService
    {
        private readonly IGenericRepository<EstudianteHabilidade> _habilidadRepo;
        private readonly IGenericRepository<PerfilesEstudiante> _perfilRepo;
        public EstudianteHabilidadService(
            IGenericRepository<EstudianteHabilidade> habilidadRepo,
            IGenericRepository<PerfilesEstudiante> perfilRepo)
        {
            _habilidadRepo = habilidadRepo;
            _perfilRepo = perfilRepo;
        }
        private void ValidarNivelDominio(int nivel)
        {
            if (nivel < 1 || nivel > 5)
            {
                throw new Exception("El Nivel de Dominio debe ser un número entre 1 y 5.");
            }
        }
        public async Task<bool> AddHabilidadAsync(int usuarioId, EstudianteHabilidadDTO dto)
        {
            ValidarNivelDominio(dto.NivelDominio);
            var perfil = await _perfilRepo.Get(p => p.UsuarioId == usuarioId);
            if (perfil == null) throw new Exception("No se encontró el perfil del estudiante.");
            var existe = await _habilidadRepo.Exists(h => h.PerfilId == perfil.Id && h.HabilidadId == dto.HabilidadId);
            if (existe) throw new Exception("Ya tienes esta habilidad registrada en tu perfil.");
            var nuevaHabilidad = new EstudianteHabilidade
            {
                PerfilId = perfil.Id,
                HabilidadId = dto.HabilidadId,
                NivelDominio = dto.NivelDominio 
            };
            var habilidadCreada = await _habilidadRepo.Create(nuevaHabilidad);
            return habilidadCreada != null;
        }
        public async Task<bool> UpdateHabilidadAsync(int usuarioId, int habilidadId, EstudianteHabilidadDTO dto)
        {
            ValidarNivelDominio(dto.NivelDominio);
            var perfil = await _perfilRepo.Get(p => p.UsuarioId == usuarioId);
            if (perfil == null) throw new Exception("No se encontró el perfil del estudiante.");
            var habilidad = await _habilidadRepo.Get(h => h.HabilidadId == habilidadId && h.PerfilId == perfil.Id);
            if (habilidad == null) throw new Exception("La habilidad no existe en tu perfil.");
            habilidad.NivelDominio = dto.NivelDominio;
            return await _habilidadRepo.Update(habilidad);
        }
        public async Task<bool> DeleteHabilidadAsync(int usuarioId, int habilidadId)
        {
            var perfil = await _perfilRepo.Get(p => p.UsuarioId == usuarioId);
            if (perfil == null) return false;
            var habilidad = await _habilidadRepo.Get(h => h.HabilidadId == habilidadId && h.PerfilId == perfil.Id);
            if (habilidad == null) throw new Exception("La habilidad no existe o no tienes permisos.");
            return await _habilidadRepo.HardDelete(habilidad);
        }
    }
}
