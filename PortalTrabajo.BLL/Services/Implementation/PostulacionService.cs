using AutoMapper;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DAL.Repositories.Contract;
using PortalTrabajo.DTO.Postulaciones;
using PortalTrabajo.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTrabajo.BLL.Services.Implementation;

public class PostulacionService : IPostulacionService
{
    private readonly IGenericRepository<Postulacione> _postulacionRepo;
    private readonly IGenericRepository<PerfilesEstudiante> _perfilRepo;
    private readonly IGenericRepository<OfertasLaborale> _ofertaRepo;
    private readonly IMapper _mapper;

    public PostulacionService(
        IGenericRepository<Postulacione> postulacionRepo,
        IGenericRepository<PerfilesEstudiante> perfilRepo,
        IGenericRepository<OfertasLaborale> ofertaRepo,
        IMapper mapper)
    {
        _postulacionRepo = postulacionRepo;
        _perfilRepo = perfilRepo;
        _ofertaRepo = ofertaRepo;
        _mapper = mapper;
    }

    public async Task<PostulacionDTO> PostularseAsync(int usuarioId, CreatePostulacionDTO model)
    {
        var perfil = await _perfilRepo.Get(p => p.UsuarioId == usuarioId);
        if (perfil == null)
            throw new Exception("El usuario no tiene un perfil de estudiante.");

        var oferta = await _ofertaRepo.Get(o => o.Id == model.OfertaId);
        if (oferta == null || oferta.Activa == false)
            throw new Exception("La oferta laboral no existe o no está activa.");

        var yaPostulado = await _postulacionRepo.Exists(p => p.PerfilId == perfil.Id && p.OfertaId == model.OfertaId);
        if (yaPostulado)
            throw new Exception("Ya te has postulado a esta oferta laboral anteriormente.");

        var postulacion = new Postulacione
        {
            OfertaId = model.OfertaId,
            PerfilId = perfil.Id,
            Mensaje = model.Mensaje,
            CurriculumUrl = model.CurriculumUrl,
            FechaPostulacion = DateTime.Now,
            EstadoId = 1 // Asumiendo que 1 es 'Pendiente' / 'Enviado'
        };

        var postulacionCreada = await _postulacionRepo.Create(postulacion);

        // Fetch de nuevo para obtener relaciones como Estado, Oferta (para DTO mapeo)
        var postulacionDb = await _postulacionRepo.Get(
            p => p.Id == postulacionCreada.Id,
            p => p.Oferta.Empresa,
            p => p.Perfil,
            p => p.Estado
        );

        return _mapper.Map<PostulacionDTO>(postulacionDb);
    }

    public async Task<IEnumerable<PostulacionDTO>> GetPostulacionesPorUsuarioAsync(int usuarioId)
    {
        var perfil = await _perfilRepo.Get(p => p.UsuarioId == usuarioId);
        if (perfil == null)
            throw new Exception("El usuario no tiene un perfil de estudiante.");

        var postulaciones = await _postulacionRepo.GetAll(
            p => p.PerfilId == perfil.Id,
            p => p.Oferta.Empresa,
            p => p.Perfil,
            p => p.Estado
        );

        return _mapper.Map<IEnumerable<PostulacionDTO>>(postulaciones);
    }
}
