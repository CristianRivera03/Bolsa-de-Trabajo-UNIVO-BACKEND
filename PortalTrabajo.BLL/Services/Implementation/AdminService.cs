using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DAL.Repositories.Contract;
using PortalTrabajo.DTO.Admin;
using PortalTrabajo.DTO.Usuarios;
using PortalTrabajo.DTO.OfertasLaborales;
using PortalTrabajo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PortalTrabajo.BLL.Services.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly IGenericRepository<Usuario> _userRepository;
        private readonly IGenericRepository<Empresa> _empresaRepository;
        private readonly IGenericRepository<OfertasLaborale> _ofertaRepository;
        private readonly IGenericRepository<Postulacione> _postulacionRepository;
        private readonly IMapper _mapper;
        public AdminService(
            IGenericRepository<Usuario> userRepository,
            IGenericRepository<Empresa> empresaRepository,
            IGenericRepository<OfertasLaborale> ofertaRepository,
            IGenericRepository<Postulacione> postulacionRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _empresaRepository = empresaRepository;
            _ofertaRepository = ofertaRepository;
            _postulacionRepository = postulacionRepository;
            _mapper = mapper;
        }
        public async Task<AdminDashboardStatsDTO> GetDashboardStatsAsync()
        {
            var totalUsers = await _userRepository.Query().CountAsync();
            var activeUsers = await _userRepository.Query(u => u.Activo == true).CountAsync();
            var inactiveUsers = totalUsers - activeUsers;
            var totalCompanies = await _empresaRepository.Query().CountAsync();
            var activeCompanies = await _empresaRepository.Query()
                .Include(e => e.Usuario)
                .Where(e => e.Usuario.Activo == true)
                .CountAsync();
            var inactiveCompanies = totalCompanies - activeCompanies;
            var totalOffers = await _ofertaRepository.Query().CountAsync();
            var activeOffers = await _ofertaRepository.Query(o => o.Activa == true).CountAsync();
            var inactiveOffers = totalOffers - activeOffers;
            var totalPostulations = await _postulacionRepository.Query().CountAsync();
            return new AdminDashboardStatsDTO
            {
                TotalUsuarios = totalUsers,
                UsuariosActivos = activeUsers,
                UsuariosInactivos = inactiveUsers,
                TotalEmpresas = totalCompanies,
                EmpresasActivas = activeCompanies,
                EmpresasInactivas = inactiveCompanies,
                TotalOfertas = totalOffers,
                OfertasActivas = activeOffers,
                OfertasInactivas = inactiveOffers,
                TotalPostulaciones = totalPostulations
            };
        }
        public async Task<IEnumerable<UsuarioDTO>> GetUsersAsync()
        {
            var users = await _userRepository.Query()
                .Include(u => u.Rol)
                .ToListAsync();
            return _mapper.Map<IEnumerable<UsuarioDTO>>(users);
        }
        public async Task<bool> ToggleUserStatusAsync(int userId, bool active)
        {
            var user = await _userRepository.GetById(userId);
            if (user == null) return false;
            user.Activo = active;
            var success = await _userRepository.Update(user);
            if (success && !active)
            {
                var empresa = await _empresaRepository.Get(e => e.UsuarioId == userId);
                if (empresa != null)
                {
                    var ofertas = await _ofertaRepository.Query(o => o.EmpresaId == empresa.Id && o.Activa == true).ToListAsync();
                    foreach (var o in ofertas)
                    {
                        o.Activa = false;
                        await _ofertaRepository.Update(o);
                    }
                }
            }
            return success;
        }
        public async Task<IEnumerable<AdminEmpresaDTO>> GetCompaniesAsync()
        {
            var companies = await _empresaRepository.Query()
                .Include(e => e.Usuario)
                .ToListAsync();
            return companies.Select(e => new AdminEmpresaDTO
            {
                Id = e.Id,
                UsuarioId = e.UsuarioId,
                NombreComercial = e.NombreComercial,
                RazonSocial = e.RazonSocial ?? "",
                Nit = e.Nit ?? "",
                Email = e.Usuario?.Email ?? "N/A",
                Activo = e.Usuario?.Activo ?? false,
                FechaRegistro = e.Usuario?.FechaRegistro
            }).ToList();
        }
        public async Task<bool> ToggleCompanyStatusAsync(int companyId, bool active)
        {
            var empresa = await _empresaRepository.GetById(companyId);
            if (empresa == null) return false;
            var user = await _userRepository.GetById(empresa.UsuarioId);
            if (user == null) return false;
            user.Activo = active;
            var success = await _userRepository.Update(user);
            if (success && !active)
            {
                var ofertas = await _ofertaRepository.Query(o => o.EmpresaId == companyId && o.Activa == true).ToListAsync();
                foreach (var o in ofertas)
                {
                    o.Activa = false;
                    await _ofertaRepository.Update(o);
                }
            }
            return success;
        }
        public async Task<IEnumerable<OfertaLaboralDTO>> GetJobPostsAsync()
        {
            var posts = await _ofertaRepository.Query()
                .Include(o => o.Empresa)
                .Include(o => o.Modalidad)
                .Include(o => o.Licencia)
                .Include(o => o.TipoContrato)
                .Include(o => o.Distrito)
                .Include(o => o.Genero)
                .Include(o => o.Carreras)
                .ToListAsync();
            return _mapper.Map<IEnumerable<OfertaLaboralDTO>>(posts);
        }
        public async Task<bool> ToggleJobPostStatusAsync(int jobId, bool active)
        {
            var post = await _ofertaRepository.GetById(jobId);
            if (post == null) return false;
            post.Activa = active;
            return await _ofertaRepository.Update(post);
        }
    }
}
