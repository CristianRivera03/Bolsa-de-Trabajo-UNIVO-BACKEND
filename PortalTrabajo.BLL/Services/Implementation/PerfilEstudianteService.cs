using AutoMapper;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DAL.Repositories.Contract;
using PortalTrabajo.DTO.PerfilesEstudiante;
using PortalTrabajo.Model;
using System;
using System.Threading.Tasks;

namespace PortalTrabajo.BLL.Services.Implementation;

public class PerfilEstudianteService : IPerfilEstudianteService
{
    private readonly IGenericRepository<PerfilesEstudiante> _perfilRepo;
    private readonly IMapper _mapper;

    public PerfilEstudianteService(IGenericRepository<PerfilesEstudiante> perfilRepo, IMapper mapper)
    {
        _perfilRepo = perfilRepo;
        _mapper = mapper;
    }

    public async Task<PerfilEstudianteDTO> GetPerfilByUsuarioIdAsync(int usuarioId)
    {
        var perfil = await _perfilRepo.Get(
            p => p.UsuarioId == usuarioId,
            p => p.Carrera,
            p => p.Educacions,
            p => p.ExperienciasLaborales,
            p => p.EstudianteHabilidades,
            p => p.EstudianteIdiomas
        );

        if (perfil == null)
            throw new Exception("Perfil no encontrado para el usuario especificado.");

        return _mapper.Map<PerfilEstudianteDTO>(perfil);
    }

    public async Task<PerfilEstudianteDTO> UpdatePerfilAsync(int usuarioId, PerfilEstudianteUpdateDTO dto)
    {
        var perfil = await _perfilRepo.Get(p => p.UsuarioId == usuarioId);

        if (perfil == null)
            throw new Exception("Perfil no encontrado.");

        perfil.Telefono = dto.Telefono;
        perfil.Direccion = dto.Direccion;
        perfil.SobreMi = dto.SobreMi;
        perfil.FotoUrl = dto.FotoUrl;
        perfil.EnlaceGitHub = dto.EnlaceGitHub;
        perfil.EnlaceLinkedIn = dto.EnlaceLinkedIn;
        perfil.CarreraId = dto.CarreraId;
        perfil.BuscaEmpleo = dto.BuscaEmpleo;
        perfil.FechaActualizacion = DateTime.Now;

        await _perfilRepo.Update(perfil);

        return await GetPerfilByUsuarioIdAsync(usuarioId);
    }
}
