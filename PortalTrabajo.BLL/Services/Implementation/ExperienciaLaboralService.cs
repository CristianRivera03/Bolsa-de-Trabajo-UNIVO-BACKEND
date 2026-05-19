using AutoMapper;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DAL.Repositories.Contract;
using PortalTrabajo.DTO.PerfilesEstudiante;
using PortalTrabajo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortalTrabajo.BLL.Services.Implementation
{
    public class ExperienciaLaboralService : IExperienciaLaboralService
    {
        private readonly IGenericRepository<ExperienciasLaborale> _experenciasRepo;
        private readonly IGenericRepository<PerfilesEstudiante> _perfilRepo;
        private readonly IMapper _mapper;

        public ExperienciaLaboralService(IGenericRepository<ExperienciasLaborale> experenciasRepo, IGenericRepository<PerfilesEstudiante> perfilRepo, IMapper mapper)
        {
            _experenciasRepo = experenciasRepo;
            _perfilRepo = perfilRepo;
            _mapper = mapper;
        }

        public async Task<bool> AddExperienciaAsync(int usuarioId, ExperienciaLaboralDTO dto)
        {
            var perfil = await _perfilRepo.Get(p => p.UsuarioId == usuarioId);
            if (perfil == null) throw new Exception("No se encontró el perfil del estudiante.");

            var nuevaExperiencia = new ExperienciasLaborale
            {
                PerfilId = perfil.Id,
                Empresa = dto.Empresa,
                Cargo = dto.Cargo,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                EsTrabajoActual = dto.EsTrabajoActual ?? false,
                DescripcionPuesto = dto.DescripcionPuesto
            };

            var experienciaCreada = await _experenciasRepo.Create(nuevaExperiencia);

            return experienciaCreada != null && experienciaCreada.Id > 0;
        }

        public async Task<bool> DeleteExperienciaAsync(int usuarioId, int idExperiencia)
        {
            var perfil = await _perfilRepo.Get(p => p.UsuarioId == usuarioId);
            if (perfil == null) return false;

            var experiencia = await _experenciasRepo.Get(e => e.Id == idExperiencia && e.PerfilId == perfil.Id);
            if (experiencia == null) throw new Exception("La experiencia no existe o no tienes permisos para eliminarla.");

            return await _experenciasRepo.HardDelete(experiencia);
        }

        public async Task<bool> UpdateExperienciaAsync(int usuarioId, int idExperiencia, ExperienciaLaboralDTO dto)
        {
            var perfil = await _perfilRepo.Get(p => p.UsuarioId == usuarioId);
            if (perfil == null) throw new Exception("No se encontró el perfil del estudiante.");

            var experiencia = await _experenciasRepo.Get(e => e.Id == idExperiencia && e.PerfilId == perfil.Id);
            if (experiencia == null) throw new Exception("La experiencia no existe o no te pertenece.");

            experiencia.Empresa = dto.Empresa;
            experiencia.Cargo = dto.Cargo;
            experiencia.FechaInicio = dto.FechaInicio;
            experiencia.FechaFin = dto.FechaFin;
            experiencia.EsTrabajoActual = dto.EsTrabajoActual ?? false;
            experiencia.DescripcionPuesto = dto.DescripcionPuesto;

            return await _experenciasRepo.Update(experiencia);
        }
    }
}
