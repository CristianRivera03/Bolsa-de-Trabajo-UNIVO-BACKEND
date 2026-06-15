using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DAL.Repositories.Contract;
using PortalTrabajo.DTO.PerfilesEstudiante;
using PortalTrabajo.Model;
using System;
using System.Collections.Generic;
using System.Text;
namespace PortalTrabajo.BLL.Services.Implementation
{
    public class EstudianteIdiomaService : IEstudianteIdiomaService
    {
        private readonly IGenericRepository<EstudianteIdioma> _idiomaRepo;
        private readonly IGenericRepository<PerfilesEstudiante> _perfilRepo;
        public EstudianteIdiomaService(
            IGenericRepository<EstudianteIdioma> idiomaRepo,
            IGenericRepository<PerfilesEstudiante> perfilRepo)
        {
            _idiomaRepo = idiomaRepo;
            _perfilRepo = perfilRepo;
        }
        public async Task<bool> AddIdiomaAsync(int usuarioId, EstudianteIdiomaDTO dto)
        {
            var perfil = await _perfilRepo.Get(p => p.UsuarioId == usuarioId);
            if (perfil == null) throw new Exception("No se encontró el perfil del estudiante.");
            var nuevoIdioma = new EstudianteIdioma
            {
                PerfilId = perfil.Id,
                Idioma = dto.Idioma,
                NivelId = dto.NivelId
            };
            var idiomaCreado = await _idiomaRepo.Create(nuevoIdioma);
            return idiomaCreado != null && idiomaCreado.Id > 0;
        }
        public async Task<bool> UpdateIdiomaAsync(int usuarioId, int idIdioma, EstudianteIdiomaDTO dto)
        {
            var perfil = await _perfilRepo.Get(p => p.UsuarioId == usuarioId);
            if (perfil == null) throw new Exception("No se encontró el perfil del estudiante.");
            var idioma = await _idiomaRepo.Get(i => i.Id == idIdioma && i.PerfilId == perfil.Id);
            if (idioma == null) throw new Exception("El idioma no existe o no te pertenece.");
            idioma.Idioma = dto.Idioma;
            idioma.NivelId = dto.NivelId;
            return await _idiomaRepo.Update(idioma);
        }
        public async Task<bool> DeleteIdiomaAsync(int usuarioId, int idIdioma)
        {
            var perfil = await _perfilRepo.Get(p => p.UsuarioId == usuarioId);
            if (perfil == null) return false;
            var idioma = await _idiomaRepo.Get(i => i.Id == idIdioma && i.PerfilId == perfil.Id);
            if (idioma == null) throw new Exception("El idioma no existe o no tienes permisos.");
            return await _idiomaRepo.HardDelete(idioma);
        }
    }
}
