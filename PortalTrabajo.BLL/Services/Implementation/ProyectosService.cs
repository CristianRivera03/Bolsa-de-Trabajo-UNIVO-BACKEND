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
    public class ProyectosService : IProyectosService
    {
        private readonly IGenericRepository<ProyectosEstudiante> _proyectosRepo;
        private readonly IGenericRepository<PerfilesEstudiante> _perfilRepo;
        private readonly IMapper _mapper;
        public ProyectosService(IGenericRepository<ProyectosEstudiante> proyectosRepo, IGenericRepository<PerfilesEstudiante> perfilRepo, IMapper mapper)
        {
            _proyectosRepo = proyectosRepo;
            _perfilRepo = perfilRepo;
            _mapper = mapper;
        }
        public async Task<bool> AddProyectoAsync(int usuarioId, ProyectoEstudianteDTO dto)
        {
            var perfil = await _perfilRepo.Get(p => p.UsuarioId == usuarioId);
            if (perfil == null) throw new Exception("No se encontró el perfil del estudiante.");
            var nuevoProyecto = new ProyectosEstudiante
            {
                PerfilId = perfil.Id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                TecnologiasUsadas = dto.TecnologiasUsadas,
                EnlaceRepositorio = dto.EnlaceRepositorio,
                FechaProyecto = dto.FechaProyecto
            };
            var proyectoCreado = await _proyectosRepo.Create(nuevoProyecto);
            return proyectoCreado != null && proyectoCreado.Id > 0;
        }
        public async Task<bool> UpdateProyectoAsync(int usuarioId, int idProyecto, ProyectoEstudianteDTO dto)
        {
            var perfil = await _perfilRepo.Get(p => p.UsuarioId == usuarioId);
            if (perfil == null) throw new Exception("No se encontró el perfil del estudiante.");
            var proyecto = await _proyectosRepo.Get(p => p.Id == idProyecto && p.PerfilId == perfil.Id);
            if (proyecto == null) throw new Exception("El proyecto no existe o no te pertenece.");
            proyecto.Nombre = dto.Nombre;
            proyecto.Descripcion = dto.Descripcion;
            proyecto.TecnologiasUsadas = dto.TecnologiasUsadas;
            proyecto.EnlaceRepositorio = dto.EnlaceRepositorio;
            proyecto.FechaProyecto = dto.FechaProyecto;
            return await _proyectosRepo.Update(proyecto);
        }
        public async Task<bool> DeleteProyectoAsync(int usuarioId, int idProyecto)
        {
            var perfil = await _perfilRepo.Get(p => p.UsuarioId == usuarioId);
            if (perfil == null) return false;
            var proyecto = await _proyectosRepo.Get(p => p.Id == idProyecto && p.PerfilId == perfil.Id);
            if (proyecto == null) throw new Exception("El proyecto no existe o no tienes permisos para eliminarlo.");
            return await _proyectosRepo.HardDelete(proyecto);
        }
    }
}
