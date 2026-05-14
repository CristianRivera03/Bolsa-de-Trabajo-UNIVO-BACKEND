using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DAL.Repositories.Contract;
using PortalTrabajo.DTO.Catalogos;
using PortalTrabajo.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTrabajo.BLL.Services.Implementation
{
    public class CatalogoService : ICatalogoService
    {
        private readonly IGenericRepository<CatCarrera> _carreraRepo;
        private readonly IGenericRepository<CatModalidade> _modalidadRepo;
        private readonly IGenericRepository<CatNivelesIdioma> _nivelIdiomaRepo;
        private readonly IGenericRepository<CatGradosAcademico> _gradoAcademicoRepo;
        private readonly IGenericRepository<CatRole> _rolRepo;
        private readonly IGenericRepository<CatEstadosPostulacion> _estadoPostulacionRepo;
        private readonly IMapper _mapper;

        public CatalogoService(
            IGenericRepository<CatCarrera> carreraRepo,
            IGenericRepository<CatModalidade> modalidadRepo,
            IGenericRepository<CatNivelesIdioma> nivelIdiomaRepo,
            IGenericRepository<CatGradosAcademico> gradoAcademicoRepo,
            IGenericRepository<CatRole> rolRepo,
            IGenericRepository<CatEstadosPostulacion> estadoPostulacionRepo,
            IMapper mapper)
        {
            _carreraRepo = carreraRepo;
            _modalidadRepo = modalidadRepo;
            _nivelIdiomaRepo = nivelIdiomaRepo;
            _gradoAcademicoRepo = gradoAcademicoRepo;
            _rolRepo = rolRepo;
            _estadoPostulacionRepo = estadoPostulacionRepo;
            _mapper = mapper;
        }

        public async Task<List<CatalogDTO>> ObtenerCarreras()
        {
            var query = await _carreraRepo.Query(c => c.Activa).ToListAsync();
            return _mapper.Map<List<CatalogDTO>>(query);
        }

        public async Task<List<CatalogDTO>> ObtenerEstadosPostulacion()
        {
            var query = await _estadoPostulacionRepo.Query().ToListAsync();
            return _mapper.Map<List<CatalogDTO>>(query);
        }

        public async Task<List<CatalogDTO>> ObtenerGradosAcademicos()
        {
            var query = await _gradoAcademicoRepo.Query().ToListAsync();
            return _mapper.Map<List<CatalogDTO>>(query);
        }

        public async Task<List<CatalogDTO>> ObtenerModalidades()
        {
            var query = await _modalidadRepo.Query().ToListAsync();
            return _mapper.Map<List<CatalogDTO>>(query);
        }

        public async Task<List<CatalogDTO>> ObtenerNivelesIdioma()
        {
            var query = await _nivelIdiomaRepo.Query().ToListAsync();
            return _mapper.Map<List<CatalogDTO>>(query);
        }

        public async Task<List<CatalogDTO>> ObtenerRoles()
        {
            var query = await _rolRepo.Query().ToListAsync();
            return _mapper.Map<List<CatalogDTO>>(query);
        }
    }
}
