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
        private readonly IGenericRepository<CatDepartamento> _departamentoRepo;
        private readonly IGenericRepository<CatMunicipio> _municipioRepo;
        private readonly IGenericRepository<CatTiposContrato> _tipoContratoRepo;
        private readonly IGenericRepository<CatTiposLicencium> _tipoLicenciaRepo;
        private readonly IGenericRepository<CatGenero> _generoRepo;
        private readonly IGenericRepository<Habilidade> _habilidadesRepo;
        private readonly IGenericRepository<CatDistrito> _distritoRepo;
        private readonly IGenericRepository<CatSector> _sectorRepo;
        private readonly IMapper _mapper;
        public CatalogoService(IGenericRepository<CatCarrera> carreraRepo, IGenericRepository<CatModalidade> modalidadRepo, IGenericRepository<CatNivelesIdioma> nivelIdiomaRepo, IGenericRepository<CatGradosAcademico> gradoAcademicoRepo, IGenericRepository<CatRole> rolRepo, IGenericRepository<CatEstadosPostulacion> estadoPostulacionRepo, IGenericRepository<CatDepartamento> departamentoRepo, IGenericRepository<CatMunicipio> municipioRepo, IGenericRepository<CatTiposContrato> tipoContratoRepo, IGenericRepository<CatTiposLicencium> tipoLicenciaRepo, IGenericRepository<CatGenero> generoRepo, IGenericRepository<Habilidade> habilidadesRepo, IGenericRepository<CatDistrito> distritoRepo, IGenericRepository<CatSector> sectorRepo, IMapper mapper)
        {
            _carreraRepo = carreraRepo;
            _modalidadRepo = modalidadRepo;
            _nivelIdiomaRepo = nivelIdiomaRepo;
            _gradoAcademicoRepo = gradoAcademicoRepo;
            _rolRepo = rolRepo;
            _estadoPostulacionRepo = estadoPostulacionRepo;
            _departamentoRepo = departamentoRepo;
            _municipioRepo = municipioRepo;
            _tipoContratoRepo = tipoContratoRepo;
            _tipoLicenciaRepo = tipoLicenciaRepo;
            _generoRepo = generoRepo;
            _habilidadesRepo = habilidadesRepo;
            _distritoRepo = distritoRepo;
            _sectorRepo = sectorRepo;
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
        public async Task<List<CatalogDTO>> ObtenerDepartamentos()
        {
            var query = await _departamentoRepo.Query().ToListAsync();
            return _mapper.Map<List<CatalogDTO>>(query);
        }
        public async Task<List<CatalogDTO>> ObtenerMunicipios(int departamentoId)
        {
            var query = await _municipioRepo.Query(m => m.DepartamentoId == departamentoId).ToListAsync();
            return _mapper.Map<List<CatalogDTO>>(query);
        }
        public async Task<List<CatalogDTO>> ObtenerDistritos(int municipioId)
        {
            var query = await _distritoRepo.Query(d => d.MunicipioId == municipioId).ToListAsync();
            return _mapper.Map<List<CatalogDTO>>(query);
        }
        public async Task<List<CatalogDTO>> ObtenerTiposContrato()
        {
            var query = await _tipoContratoRepo.Query().ToListAsync();
            return _mapper.Map<List<CatalogDTO>>(query);
        }
        public async Task<List<CatalogDTO>> ObtenerTiposLicencia()
        {
            var query = await _tipoLicenciaRepo.Query().ToListAsync();
            return _mapper.Map<List<CatalogDTO>>(query);
        }
        public async Task<List<CatalogDTO>> ObtenerGeneros()
        {
            var query = await _generoRepo.Query().ToListAsync();
            return _mapper.Map<List<CatalogDTO>>(query);
        }
        public async Task<List<CatalogDTO>> ObtenerHabilidades()
        {
            var query = await _habilidadesRepo.Query().ToListAsync();
            return _mapper.Map<List<CatalogDTO>>(query);
        }

        public async Task<List<CatalogDTO>> ObtenerSectores()
        {
            var query = await _sectorRepo.Query().ToListAsync();
            return _mapper.Map<List<CatalogDTO>>(query);
        }

        public async Task<CatalogDTO> CrearSector(CatalogDTO dto)
        {
            var entity = new CatSector { Nombre = dto.Nombre };
            var result = await _sectorRepo.Create(entity);
            return new CatalogDTO { Id = result.Id, Nombre = result.Nombre };
        }

        public async Task<bool> EliminarSector(int id)
        {
            var entity = await _sectorRepo.Get(x => x.Id == id);
            if (entity == null) return false;
            return await _sectorRepo.HardDelete(entity);
        }

        public async Task<CatalogDTO> CrearHabilidad(CatalogDTO dto)
        {
            var entity = new Habilidade { Nombre = dto.Nombre };
            var result = await _habilidadesRepo.Create(entity);
            return new CatalogDTO { Id = result.Id, Nombre = result.Nombre };
        }

        public async Task<bool> EliminarHabilidad(int id)
        {
            var entity = await _habilidadesRepo.Get(x => x.Id == id);
            if (entity == null) return false;
            return await _habilidadesRepo.HardDelete(entity);
        }

        public async Task<CatalogDTO> CrearCarrera(CatalogDTO dto)
        {
            var entity = new CatCarrera { Nombre = dto.Nombre, Activa = true };
            var result = await _carreraRepo.Create(entity);
            return new CatalogDTO { Id = result.Id, Nombre = result.Nombre };
        }

        public async Task<bool> EliminarCarrera(int id)
        {
            var entity = await _carreraRepo.Get(x => x.Id == id);
            if (entity == null) return false;
            return await _carreraRepo.HardDelete(entity);
        }
    }
}
