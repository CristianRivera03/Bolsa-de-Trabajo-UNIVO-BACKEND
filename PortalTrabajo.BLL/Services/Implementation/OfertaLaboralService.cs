using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DAL.DBContext;
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
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public OfertaLaboralService(
            IGenericRepository<OfertasLaborale> ofertaRepositorio, 
            IGenericRepository<Empresa> empresaRepositorio, 
            IGenericRepository<CatCarrera> carreraRepositorio, 
            IMapper mapper,
            IServiceScopeFactory serviceScopeFactory)
        {
            _ofertaRepositorio = ofertaRepositorio;
            _empresaRepositorio = empresaRepositorio;
            _carreraRepositorio = carreraRepositorio;
            _mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public async Task<List<OfertaLaboralDTO>> ObtenerTodos(string palabraClave = null, int? carreraId = null, int? sectorId = null)
        {
            try
            {
                var query =  _ofertaRepositorio.Query();
                
                var listaOfertasQuery = query
                    .Include(o => o.Empresa)
                    .Include(o => o.Modalidad)
                    .Include(o => o.OfertaCarreras).ThenInclude(oc => oc.Carrera)
                    .Include(o => o.Genero)
                    .Include(o => o.Distrito)
                    .Include(o => o.Licencia)
                    .Include(o => o.TipoContrato)
                    .Include(o => o.OfertaHabilidades)
                        .ThenInclude(oh => oh.Habilidad)
                    .Where(o => o.Activo == true && (o.FechaExpiracion == null || o.FechaExpiracion >= DateTime.Now));

                if (!string.IsNullOrEmpty(palabraClave))
                {
                    string lowerKeyword = palabraClave.ToLower();
                    listaOfertasQuery = listaOfertasQuery.Where(o => 
                        o.Titulo.ToLower().Contains(lowerKeyword) || 
                        o.Empresa.NombreComercial.ToLower().Contains(lowerKeyword));
                }

                if (carreraId.HasValue && carreraId.Value > 0)
                {
                    listaOfertasQuery = listaOfertasQuery.Where(o => o.OfertaCarreras.Any(oc => oc.CarreraId == carreraId.Value));
                }

                if (sectorId.HasValue && sectorId.Value > 0)
                {
                    listaOfertasQuery = listaOfertasQuery.Where(o => o.Empresa.SectorId == sectorId.Value);
                }

                var listaOfertas = await listaOfertasQuery
                    .OrderByDescending(o => o.FechaPublicacion)
                    .ToListAsync();

                return _mapper.Map<List<OfertaLaboralDTO>>(listaOfertas);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las ofertas laborales", ex);
            }
        }
        public async Task<List<OfertaLaboralDTO>> ObtenerMisOfertasAsync(int usuarioEmpresaId)
        {
            try
            {
                var query = _ofertaRepositorio.Query();
                var listaOfertas = await query
                    .Include(o => o.Empresa)
                    .Include(o => o.Modalidad)
                    .Include(o => o.OfertaCarreras).ThenInclude(oc => oc.Carrera)
                    .Include(o => o.Genero)
                    .Include(o => o.Distrito)
                    .Include(o => o.Licencia)
                    .Include(o => o.TipoContrato)
                    .Include(o => o.OfertaHabilidades)
                        .ThenInclude(oh => oh.Habilidad)
                    .Where(o => o.Empresa.UsuarioId == usuarioEmpresaId)
                    .OrderByDescending(o => o.FechaPublicacion)
                    .ToListAsync();
                return _mapper.Map<List<OfertaLaboralDTO>>(listaOfertas);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las ofertas de la empresa", ex);
            }
        }
        public async Task<OfertaLaboralDTO> Crear(OfertaLaboralCreateDTO modelo, int usuarioEmpresaId)
        {
            try
            {
                var empresaReal = await _empresaRepositorio.Get(e => e.UsuarioId == usuarioEmpresaId);
                if (empresaReal == null)
                    throw new UnauthorizedAccessException("La empresa no existe o no tienes permisos");
                var dbModelo = _mapper.Map<OfertasLaborale>(modelo);
                if (modelo.CarreraIds != null && modelo.CarreraIds.Any())
                {
                    dbModelo.OfertaCarreras = modelo.CarreraIds.Select(cId => new OfertaCarrera
                    {
                        CarreraId = cId
                    }).ToList();
                }
                if (modelo.HabilidadIds != null && modelo.HabilidadIds.Any())
                {
                    dbModelo.OfertaHabilidades = modelo.HabilidadIds.Select(hId => new OfertaHabilidade
                    {
                        HabilidadId = hId,
                        EsObligatorio = true
                    }).ToList();
                }
                dbModelo.EmpresaId = empresaReal.Id;
                dbModelo.FechaPublicacion = DateTime.Now;
                dbModelo.Activo = true;
                var ofertaCreada = await _ofertaRepositorio.Create(dbModelo);
                if (ofertaCreada.Id == 0)
                    throw new TaskCanceledException("No se pudo crear la oferta laboral");
                NotificarEstudiantesNuevasOfertas(ofertaCreada.Id, modelo.HabilidadIds ?? new List<int>(), modelo.CarreraIds ?? new List<int>(), empresaReal.NombreComercial, ofertaCreada.Titulo);
                return _mapper.Map<OfertaLaboralDTO>(ofertaCreada);
            }
            catch (Exception ex)
            {
                var mensajeError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                throw new Exception($"Error DB: {mensajeError}");
            }
        }
        public async Task<bool> DesactivarOfertasExpiradas()
        {
            try
            {
                var fechaActual = DateTime.Now;
                var ofertasVencidas = await _ofertaRepositorio.Query( o =>
                    o.Activo == true &&
                    o.FechaExpiracion != null &&
                    o.FechaExpiracion <= fechaActual
                    ).ToListAsync();
                if (ofertasVencidas.Any())
                {
                    foreach (var oferta in ofertasVencidas)
                    {
                        oferta.Activo = false;
                        oferta.FechaModificacion = fechaActual;
                        await _ofertaRepositorio.Update(oferta);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al desactivar ofertas expiradas", ex);
            }
        }
        public async Task<OfertaLaboralDTO> ObtenerPorId(int id){
            try
            {
                var query = _ofertaRepositorio.Query();
                var ofertaEncontrada = await query
                    .Include(o => o.Empresa)
                    .Include(o => o.Modalidad)
                    .Include(o => o.OfertaCarreras).ThenInclude(oc => oc.Carrera)
                    .Include(o => o.Genero)
                    .Include(o => o.Distrito)
                        .ThenInclude(d => d.Municipio)
                            .ThenInclude(m => m.Departamento)
                    .Include(o => o.Licencia)
                    .Include(o => o.TipoContrato)
                    .Include(o => o.OfertaHabilidades)
                        .ThenInclude(oh => oh.Habilidad)
                    .FirstOrDefaultAsync(o => o.Id == id); 
                if (ofertaEncontrada == null)
                {
                    return null; 
                }
                return _mapper.Map<OfertaLaboralDTO>(ofertaEncontrada);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la oferta laboral por ID", ex);
            }
        }
        private void NotificarEstudiantesNuevasOfertas(int ofertaId, List<int> habilidadIds, List<int> carreraIds, string nombreEmpresa, string tituloOferta)
        {
            Task.Run(async () =>
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<OfertaLaboralService>>();
                    try
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<PortalTrabajoDbContext>();
                        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                        var queryEstudiantes = dbContext.PerfilesEstudiantes
                            .Include(p => p.Usuario)
                            .Include(p => p.EstudianteHabilidades)
                            .Where(p => p.BuscaEmpleo && p.Usuario.Activo == true);
                        if (habilidadIds != null && habilidadIds.Any())
                        {
                            queryEstudiantes = queryEstudiantes.Where(p => p.EstudianteHabilidades.Any(eh => habilidadIds.Contains(eh.HabilidadId)));
                        }
                        if (carreraIds != null && carreraIds.Any())
                        {
                            queryEstudiantes = queryEstudiantes.Where(p => p.CarreraId != null && carreraIds.Contains(p.CarreraId.Value));
                        }
                        var estudiantesParaNotificar = await queryEstudiantes.ToListAsync();
                        logger.LogInformation("Encontrados {Count} estudiantes para notificar sobre la oferta {OfertaId}", estudiantesParaNotificar.Count, ofertaId);
                        foreach (var estudiante in estudiantesParaNotificar)
                        {
                            try
                            {
                                var email = estudiante.Usuario?.Email;
                                if (string.IsNullOrEmpty(email)) continue;
                                var asunto = $"Nueva oportunidad laboral: {tituloOferta} en {nombreEmpresa}";
                                var cuerpoHtml = $@"
                                            <p>Hola <strong>{estudiante.Nombres} {estudiante.Apellidos}</strong>,</p>
                                            <p>Hemos encontrado una nueva oferta laboral que coincide con tus habilidades en nuestra plataforma:</p>
                                            <div style='background-color: #f8fafc; border-left: 4px solid #4f46e5; padding: 15px; margin: 20px 0;'>
                                                <h3 style='margin-top: 0; color: #1e293b;'>{tituloOferta}</h3>
                                                <p style='margin-bottom: 0;'><strong>Empresa:</strong> {nombreEmpresa}</p>
                                            </div>
                                            <p>Te recomendamos revisar los detalles y postularte lo antes posible.</p>
                                            <p style='font-size: 0.75rem; color: #94a3b8; margin-top: 20px;'>Si ya no deseas recibir notificaciones de ofertas de trabajo, puedes desactivarlas en la configuración de tu perfil.</p>";
                                
                                string htmlFinal = PortalTrabajo.Utility.EmailTemplateBuilder.BuildTemplate("¡Nueva Oferta de Trabajo de tu Interés!", cuerpoHtml);
                                await emailService.EnviarCorreoAsync(email, asunto, htmlFinal);
                            }
                            catch (Exception emailEx)
                            {
                                logger.LogError(emailEx, "Error al enviar correo de notificación al estudiante {EstudianteId} ({Email})", estudiante.Id, estudiante.Usuario?.Email);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Error crítico en el proceso de notificación de nuevas ofertas laborales");
                    }
                }
            });
        }
    }
}

