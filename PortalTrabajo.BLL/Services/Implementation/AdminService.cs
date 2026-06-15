using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IGenericRepository<AuditLog> _auditLogRepository;
        private readonly IMapper _mapper;
        private readonly Microsoft.Extensions.DependencyInjection.IServiceScopeFactory _serviceScopeFactory;
        public AdminService(
            IGenericRepository<Usuario> userRepository,
            IGenericRepository<Empresa> empresaRepository,
            IGenericRepository<OfertasLaborale> ofertaRepository,
            IGenericRepository<Postulacione> postulacionRepository,
            IGenericRepository<AuditLog> auditLogRepository,
            IMapper mapper,
            Microsoft.Extensions.DependencyInjection.IServiceScopeFactory serviceScopeFactory)
        {
            _userRepository = userRepository;
            _empresaRepository = empresaRepository;
            _ofertaRepository = ofertaRepository;
            _postulacionRepository = postulacionRepository;
            _auditLogRepository = auditLogRepository;
            _mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
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
            var activeOffers = await _ofertaRepository.Query(o => o.Activo == true).CountAsync();
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
                .IgnoreQueryFilters()
                .Include(u => u.Rol)
                .ToListAsync();
            return _mapper.Map<IEnumerable<UsuarioDTO>>(users);
        }
        public async Task<bool> ToggleUserStatusAsync(int userId, bool active)
        {
            var user = await _userRepository.Query()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return false;
            user.Activo = active;
            var success = await _userRepository.Update(user);
            if (success && !active)
            {
                var empresa = await _empresaRepository.Query()
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(e => e.UsuarioId == userId);
                if (empresa != null)
                {
                    var ofertas = await _ofertaRepository.Query()
                        .IgnoreQueryFilters()
                        .Where(o => o.EmpresaId == empresa.Id && o.Activo == true)
                        .ToListAsync();
                    foreach (var o in ofertas)
                    {
                        o.Activo = false;
                        await _ofertaRepository.Update(o);
                    }
                }
            }
            return success;
        }
        public async Task<IEnumerable<AdminEmpresaDTO>> GetCompaniesAsync()
        {
            var companies = await _empresaRepository.Query()
                .IgnoreQueryFilters()
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
            var empresa = await _empresaRepository.Query()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(e => e.Id == companyId);
            if (empresa == null) return false;
            var user = await _userRepository.Query()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.Id == empresa.UsuarioId);
            if (user == null) return false;
            
            bool wasInactive = !user.Activo;
            user.Activo = active;
            var success = await _userRepository.Update(user);

            if (success && !active)
            {
                var ofertas = await _ofertaRepository.Query()
                    .IgnoreQueryFilters()
                    .Where(o => o.EmpresaId == companyId && o.Activo == true)
                    .ToListAsync();
                foreach (var o in ofertas)
                {
                    o.Activo = false;
                    await _ofertaRepository.Update(o);
                }
            }
            else if (success && active && wasInactive)
            {
                NotificarEmpresaAprobada(user.Email, empresa.NombreComercial);
            }

            return success;
        }

        private void NotificarEmpresaAprobada(string emailEmpresa, string nombreEmpresa)
        {
            if (string.IsNullOrEmpty(emailEmpresa)) return;

            Task.Run(async () =>
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    try
                    {
                        var emailService = scope.ServiceProvider.GetRequiredService<PortalTrabajo.BLL.Services.Contract.IEmailService>();
                        string asunto = "¡Tu empresa ha sido aprobada!";
                        string cuerpo = $@"
                            <p>Hola <strong>{nombreEmpresa}</strong>,</p>
                            <p>Nos complace informarte que tu cuenta ha sido verificada y aprobada por el administrador.</p>
                            <p>Ya puedes iniciar sesión en la plataforma y comenzar a publicar tus ofertas laborales.</p>";
                        
                        string htmlFinal = PortalTrabajo.Utility.EmailTemplateBuilder.BuildTemplate(asunto, cuerpo);
                        await emailService.EnviarCorreoAsync(emailEmpresa, asunto, htmlFinal);
                    }
                    catch { }
                }
            });
        }
        public async Task<IEnumerable<OfertaLaboralDTO>> GetJobPostsAsync()
        {
            var posts = await _ofertaRepository.Query()
                .IgnoreQueryFilters()
                .Include(o => o.Empresa)
                .Include(o => o.Modalidad)
                .Include(o => o.Licencia)
                .Include(o => o.TipoContrato)
                .Include(o => o.Distrito)
                .Include(o => o.Genero)
                .Include(o => o.OfertaCarreras).ThenInclude(oc => oc.Carrera)
                .ToListAsync();
            return _mapper.Map<IEnumerable<OfertaLaboralDTO>>(posts);
        }
        public async Task<bool> ToggleJobPostStatusAsync(int jobId, bool active)
        {
            var post = await _ofertaRepository.Query()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(o => o.Id == jobId);
            if (post == null) return false;
            post.Activo = active;
            return await _ofertaRepository.Update(post);
        }

        public async Task<PaginatedResponse<AuditLogDTO>> GetAuditLogsAsync(AuditLogFilterDTO filter)
        {
            var query = _auditLogRepository.Query();

            if (!string.IsNullOrEmpty(filter.Tabla))
            {
                query = query.Where(l => l.NombreTabla.Contains(filter.Tabla));
            }

            if (!string.IsNullOrEmpty(filter.Accion))
            {
                query = query.Where(l => l.Accion == filter.Accion);
            }

            if (filter.FechaInicio.HasValue)
            {
                query = query.Where(l => (l.FechaCreacion ?? l.FechaModificacion ?? l.Fecha) >= filter.FechaInicio.Value);
            }

            if (filter.FechaFin.HasValue)
            {
                // Incluir todo el día de la fecha de fin
                var fechaFinReal = filter.FechaFin.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(l => (l.FechaCreacion ?? l.FechaModificacion ?? l.Fecha) <= fechaFinReal);
            }

            int totalRecords = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalRecords / (double)filter.PageSize);

            var logs = await query
                .OrderByDescending(l => l.FechaCreacion ?? l.FechaModificacion ?? l.Fecha)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var items = logs.Select(l => new AuditLogDTO
            {
                Id = l.Id,
                NombreTabla = l.NombreTabla,
                Accion = l.Accion,
                RegistroId = l.RegistroId,
                ValoresAntiguos = l.ValoresAntiguos,
                ValoresNuevos = l.ValoresNuevos,
                Fecha = l.FechaCreacion ?? l.FechaModificacion ?? l.Fecha,
                UsuarioId = l.UsuarioCreacionId ?? l.UsuarioModificacionId
            }).ToList();

            return new PaginatedResponse<AuditLogDTO>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalPages = totalPages
            };
        }
    }
}

