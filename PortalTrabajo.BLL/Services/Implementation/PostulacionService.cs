using AutoMapper;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DAL.Repositories.Contract;
using PortalTrabajo.DTO.Postulaciones;
using PortalTrabajo.DTO.PerfilesEstudiante;
using PortalTrabajo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace PortalTrabajo.BLL.Services.Implementation;
public class PostulacionService : IPostulacionService
{
    private readonly IGenericRepository<Postulacione> _postulacionRepo;
    private readonly IGenericRepository<PerfilesEstudiante> _perfilRepo;
    private readonly IGenericRepository<OfertasLaborale> _ofertaRepo;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private const int ESTADO_RECIBIDA_ID = 1;
    public PostulacionService(
        IGenericRepository<Postulacione> postulacionRepo,
        IGenericRepository<PerfilesEstudiante> perfilRepo,
        IGenericRepository<OfertasLaborale> ofertaRepo,
        IMapper mapper,
        IEmailService emailService)
    {
        _postulacionRepo = postulacionRepo;
        _perfilRepo = perfilRepo;
        _ofertaRepo = ofertaRepo;
        _mapper = mapper;
        _emailService = emailService;
    }
    public async Task<bool> AplicarOfertaAsync(int usuarioId, CreatePostulacionDTO dto)
    {
        var perfil = await _perfilRepo.Query(p => p.UsuarioId == usuarioId)
            .Include(p => p.Usuario)
            .Include(p => p.EstudianteHabilidades)
            .FirstOrDefaultAsync();
        if (perfil == null)
            throw new Exception("Debes completar tu perfil de estudiante antes de postularte.");
        var oferta = await _ofertaRepo.Query(o => o.Id == dto.OfertaId)
            .Include(o => o.OfertaHabilidades)
            .Include(o => o.Empresa)
                .ThenInclude(e => e.Usuario)
            .FirstOrDefaultAsync();
        if (oferta == null)
            throw new Exception("La oferta laboral no existe.");
        var postulacionExistente = await _postulacionRepo.Get(p => p.PerfilId == perfil.Id && p.OfertaId == dto.OfertaId);
        if (postulacionExistente != null)
            throw new Exception("Ya te has postulado a esta oferta anteriormente.");
        var nuevaPostulacion = new Postulacione 
        {
            OfertaId = dto.OfertaId,
            PerfilId = perfil.Id,
            FechaPostulacion = DateTime.Now,
            EstadoId = ESTADO_RECIBIDA_ID, 
            Mensaje = dto.Mensaje
        };
        var resultado = await _postulacionRepo.Create(nuevaPostulacion);
        if (resultado != null && resultado.Id > 0)
        {
            var reqHabilidadIds = oferta.OfertaHabilidades
                .Where(oh => oh.EsObligatorio == true || oh.EsObligatorio == null)
                .Select(oh => oh.HabilidadId)
                .ToList();
            if (reqHabilidadIds.Any())
            {
                var estHabilidadIds = perfil.EstudianteHabilidades
                    .Select(eh => eh.HabilidadId)
                    .ToHashSet();
                if (reqHabilidadIds.All(id => estHabilidadIds.Contains(id)))
                {
                    var email = perfil.Usuario?.Email;
                    if (!string.IsNullOrEmpty(email))
                    {
                        var asunto = $"¡Coincidencia perfecta con la vacante {oferta.Titulo}!";
                        var cuerpoHtml = $@"
                                    <p>Hola <strong>{perfil.Nombres} {perfil.Apellidos}</strong>,</p>
                                    <p>Te has postulado a la oferta <strong>{oferta.Titulo}</strong> en <strong>{oferta.Empresa?.NombreComercial}</strong> y hemos detectado que cumples con el <strong>100% de los requisitos y habilidades solicitadas</strong>.</p>
                                    <p>Tu postulación destaca y la empresa ha sido notificada. ¡Mucho éxito en el proceso!</p>";
                                
                        string htmlFinal = PortalTrabajo.Utility.EmailTemplateBuilder.BuildTemplate("¡Felicidades, Coincidencia Perfecta!", cuerpoHtml);
                        _ = Task.Run(async () => {
                            try
                            {
                                await _emailService.EnviarCorreoAsync(email, asunto, htmlFinal);
                            }
                            catch
                            {
                            }
                        });
                    }
                }
            }
            var emailEmpresa = oferta.Empresa?.Usuario?.Email;
            if (!string.IsNullOrEmpty(emailEmpresa))
            {
                var asuntoEmpresa = $"Nueva postulación recibida: {oferta.Titulo}";
                var cuerpoEmpresa = $@"
                    <p>Hola <strong>{oferta.Empresa?.NombreComercial}</strong>,</p>
                    <p>El estudiante <strong>{perfil.Nombres} {perfil.Apellidos}</strong> se ha postulado a tu oferta laboral <strong>{oferta.Titulo}</strong>.</p>
                    <p>Puedes revisar su perfil y descargar su CV desde tu panel de administración en el Portal de Trabajo.</p>";
                
                string htmlFinalEmpresa = PortalTrabajo.Utility.EmailTemplateBuilder.BuildTemplate("¡Un nuevo talento ha aplicado a tu oferta!", cuerpoEmpresa);
                _ = Task.Run(async () => {
                    try { await _emailService.EnviarCorreoAsync(emailEmpresa, asuntoEmpresa, htmlFinalEmpresa); }
                    catch { }
                });
            }

            return true;
        }
        return false;
    }
    public async Task<IEnumerable<PostulacionDTO>> ObtenerMisPostulacionesAsync(int usuarioId)
    {
        var perfil = await _perfilRepo.Get(p => p.UsuarioId == usuarioId);
        if (perfil == null) return new List<PostulacionDTO>();
        var postulacionesDb = await _postulacionRepo.Query(p => p.PerfilId == perfil.Id)
                                          .Include(p => p.Oferta)
                                            .ThenInclude(o => o.Empresa)
                                          .Include(p => p.Estado)
                                          .ToListAsync();
        return postulacionesDb.Select(p => new PostulacionDTO
        {
            Id = p.Id,
            OfertaId = p.OfertaId,
            OfertaTitulo = p.Oferta?.Titulo ?? "Oferta no disponible",
            EmpresaNombre = p.Oferta?.Empresa?.NombreComercial ?? "Empresa no disponible", 
            PerfilId = p.PerfilId,
            EstudianteNombreCompleto = $"{perfil.Nombres} {perfil.Apellidos}",
            FechaPostulacion = p.FechaPostulacion,
            EstadoNombre = p.Estado?.Nombre ?? "Estado desconocido",
            Mensaje = p.Mensaje,
            EstudianteFotoUrl = perfil.FotoUrl,
            EmpresaLogoUrl = p.Oferta?.Empresa?.LogoUrl
        })
        .OrderByDescending(p => p.FechaPostulacion) 
        .ToList();
    }
    public async Task<IEnumerable<PostulacionDTO>> ObtenerPostulacionesPorOfertaAsync(int ofertaId, int usuarioEmpresaId)
    {
        var postulacionesDb = await _postulacionRepo.Query(p =>
                                            p.OfertaId == ofertaId &&
                                            p.Oferta.Empresa.UsuarioId == usuarioEmpresaId)
                                        .Include(p => p.Perfil)
                                            .ThenInclude(pe => pe.EstudianteHabilidades)
                                                .ThenInclude(eh => eh.Habilidad)
                                        .Include(p => p.Estado)
                                        .Include(p => p.Oferta)
                                        .ToListAsync();
        return postulacionesDb.Select(p => new PostulacionDTO
        {
            Id = p.Id,
            OfertaId = p.OfertaId,
            OfertaTitulo = p.Oferta?.Titulo ?? "Oferta no disponible",
            EmpresaNombre = "", 
            PerfilId = p.PerfilId,
            EstudianteNombreCompleto = p.Perfil != null ? $"{p.Perfil.Nombres} {p.Perfil.Apellidos}" : "Estudiante Desconocido",
            FechaPostulacion = p.FechaPostulacion,
            EstadoNombre = p.Estado?.Nombre ?? "Estado desconocido",
            Mensaje = p.Mensaje,
            EstudianteFotoUrl = p.Perfil?.FotoUrl,
            EmpresaLogoUrl = p.Oferta?.Empresa?.LogoUrl,
            EstudianteHabilidades = p.Perfil != null && p.Perfil.EstudianteHabilidades != null 
                ? _mapper.Map<List<EstudianteHabilidadDTO>>(p.Perfil.EstudianteHabilidades) 
                : new List<EstudianteHabilidadDTO>()
        })
        .OrderBy(p => p.FechaPostulacion)
        .ToList();
    }
    public async Task<bool> VerificarAccesoACvEstudianteAsync(int perfilId, int usuarioEmpresaId)
    {
        return await _postulacionRepo.Exists(p => 
            p.PerfilId == perfilId && 
            p.Oferta.Empresa.UsuarioId == usuarioEmpresaId);
    }

    public async Task<bool> CambiarEstadoPostulacionAsync(int postulacionId, int nuevoEstadoId, int usuarioEmpresaId)
    {
        var postulacion = await _postulacionRepo.Query(p => p.Id == postulacionId)
            .Include(p => p.Oferta)
                .ThenInclude(o => o.Empresa)
            .Include(p => p.Perfil)
                .ThenInclude(pe => pe.Usuario)
            .FirstOrDefaultAsync();

        if (postulacion == null)
            throw new Exception("La postulación no existe.");

        if (postulacion.Oferta.Empresa.UsuarioId != usuarioEmpresaId)
            throw new UnauthorizedAccessException("No tienes permisos para modificar esta postulación.");

        if (postulacion.EstadoId == nuevoEstadoId)
            return true; // No hay cambio

        postulacion.EstadoId = nuevoEstadoId;
        var resultado = await _postulacionRepo.Update(postulacion);

        if (resultado)
        {
            var emailEstudiante = postulacion.Perfil?.Usuario?.Email;
            if (!string.IsNullOrEmpty(emailEstudiante))
            {
                // Mapear nombre del estado de manera simple, o requerir repositorio de CatEstadoPostulacion
                string nombreEstado = nuevoEstadoId switch
                {
                    1 => "Recibida",
                    2 => "En revisión",
                    3 => "Entrevista",
                    4 => "Seleccionado",
                    5 => "Descartado",
                    _ => "Actualizado"
                };

                var asunto = $"Actualización de tu postulación: {postulacion.Oferta.Titulo}";
                var cuerpo = $@"
                    <p>Hola <strong>{postulacion.Perfil?.Nombres}</strong>,</p>
                    <p>Te informamos que tu postulación a la vacante <strong>{postulacion.Oferta.Titulo}</strong> en <strong>{postulacion.Oferta.Empresa.NombreComercial}</strong> ha cambiado al estado: <strong style='color: #10b981;'>{nombreEstado}</strong>.</p>
                    <p>Te deseamos mucho éxito en tu proceso de selección.</p>";

                string htmlFinal = PortalTrabajo.Utility.EmailTemplateBuilder.BuildTemplate("Actualización de tu Postulación", cuerpo);
                _ = Task.Run(async () => {
                    try { await _emailService.EnviarCorreoAsync(emailEstudiante, asunto, htmlFinal); }
                    catch { }
                });
            }
        }

        return resultado;
    }
}
