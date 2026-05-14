using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DAL.Repositories.Contract;
using PortalTrabajo.DTO.OfertasLaborales;
using PortalTrabajo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalTrabajo.BLL.Services.Implementation
{
    public class OfertaLaboralService : IOfertaLaboralService
    {
        private readonly IGenericRepository<OfertasLaborale> _ofertaRepositorio;
        private readonly IGenericRepository<Empresa> _empresaRepositorio;
        private readonly IGenericRepository<CatCarrera> _carreraRepositorio;
        private readonly IMapper _mapper;

        public OfertaLaboralService(IGenericRepository<OfertasLaborale> ofertaRepositorio, IGenericRepository<Empresa> empresaRepositorio, IGenericRepository<CatCarrera> carreraRepositorio, IMapper mapper)
        {
            _ofertaRepositorio = ofertaRepositorio;
            _empresaRepositorio = empresaRepositorio;
            _carreraRepositorio = carreraRepositorio;
            _mapper = mapper;
        }

        public async Task<List<OfertaLaboralDTO>> ObtenerTodos()
        {
            try
            {
                var query =  _ofertaRepositorio.Query();
                var listaOfertas = await query
                    .Include(o => o.Empresa)
                    .Include(o => o.Modalidad)
                    .Include(o => o.Carreras)
                    .ToListAsync();
                
                return _mapper.Map<List<OfertaLaboralDTO>>(listaOfertas);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las ofertas laborales", ex);
            }
        }
        public async Task<OfertaLaboralDTO> Crear(OfertaLaboralCreateDTO modelo)
        {
            try
            {
                var empresaReal = await _empresaRepositorio.Get(e => e.UsuarioId == modelo.EmpresaId);

                if (empresaReal == null)
                    throw new Exception("No se encontró un perfil de empresa asociado a este usuario.");

                var dbModelo = _mapper.Map<OfertasLaborale>(modelo);

                if (modelo.CarreraIds != null && modelo.CarreraIds.Any())
                {
                    dbModelo.Carreras = await _carreraRepositorio.Query(c => modelo.CarreraIds.Contains(c.Id)).ToListAsync();
                }

                dbModelo.EmpresaId = empresaReal.Id;

                dbModelo.FechaPublicacion = DateTime.Now;
                dbModelo.Activa = true;

                var ofertaCreada = await _ofertaRepositorio.Create(dbModelo);

                if (ofertaCreada.Id == 0)
                    throw new TaskCanceledException("No se pudo crear la oferta laboral");

                return _mapper.Map<OfertaLaboralDTO>(ofertaCreada);
            }
            catch (Exception ex)
            {
                var mensajeError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                throw new Exception($"Error DB: {mensajeError}");
            }
        }
    }
}
