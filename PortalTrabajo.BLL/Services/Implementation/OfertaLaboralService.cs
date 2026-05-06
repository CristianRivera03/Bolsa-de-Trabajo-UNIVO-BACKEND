using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DAL.Repositories.Contract;
using PortalTrabajo.DTO.OfertasLaborales;
using PortalTrabajo.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTrabajo.BLL.Services.Implementation
{
    public class OfertaLaboralService : IOfertaLaboralService
    {
        private readonly IGenericRepository<OfertasLaborale> _ofertaRepositorio;
        private readonly IMapper _mapper;

        public OfertaLaboralService(IGenericRepository<OfertasLaborale> ofertaRepositorio, IMapper mapper)
        {
            _ofertaRepositorio = ofertaRepositorio;
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
                    .ToListAsync();
                
                return _mapper.Map<List<OfertaLaboralDTO>>(listaOfertas);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las ofertas laborales", ex);
            }
        }
    }
}
