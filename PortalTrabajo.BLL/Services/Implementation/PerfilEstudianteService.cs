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
    private readonly PortalTrabajo.Utility.Interfaces.ICloudinaryUtility _cloudinaryUtility;

    public PerfilEstudianteService(
        IGenericRepository<PerfilesEstudiante> perfilRepo, 
        IMapper mapper,
        PortalTrabajo.Utility.Interfaces.ICloudinaryUtility cloudinaryUtility)
    {
        _perfilRepo = perfilRepo;
        _mapper = mapper;
        _cloudinaryUtility = cloudinaryUtility;
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

    public async Task<string> CambiarFotoAsync(int usuarioId, PortalTrabajo.DTO.Shared.CambiarImagenDTO dto)
    {
        var perfil = await _perfilRepo.Get(p => p.UsuarioId == usuarioId);
        if (perfil == null) throw new Exception("Perfil no encontrado.");

        if (dto.Archivo == null || dto.Archivo.Length == 0)
            throw new Exception("No se ha proporcionado ninguna imagen.");

        // Subir a Cloudinary
        string nuevaUrl = await _cloudinaryUtility.SubirImagenAsync(dto.Archivo, "Perfiles");

        if (string.IsNullOrEmpty(nuevaUrl))
            throw new Exception("Error al subir la imagen a Cloudinary.");

        // Eliminar imagen anterior si existe y no es una por defecto
        if (!string.IsNullOrEmpty(perfil.FotoUrl) && perfil.FotoUrl.Contains("cloudinary.com"))
        {
            // Extraer el public_id de la URL de Cloudinary
            // Ejemplo URL: https://res.cloudinary.com/demo/image/upload/v1573030467/PortalTrabajo/Perfiles/sample.jpg
            var segments = new Uri(perfil.FotoUrl).Segments;
            var publicIdWithExtension = string.Join("", segments.Skip(segments.Length - 3)); // toma PortalTrabajo/Perfiles/xyz.jpg
            var publicId = System.IO.Path.ChangeExtension(publicIdWithExtension, null).Replace("/", "/"); 
            // Esto asume una estructura sencilla. La utilidad real de cloudinary.Destroy necesita el publicId exacto.
            // Para evitar problemas si la extracción falla, ignoramos errores de eliminación.
            try
            {
                var idToDestroy = publicIdWithExtension.Substring(0, publicIdWithExtension.LastIndexOf('.')); // Remueve la extensión
                await _cloudinaryUtility.EliminarImagenAsync(idToDestroy);
            }
            catch { }
        }

        perfil.FotoUrl = nuevaUrl;
        perfil.FechaActualizacion = DateTime.Now;

        await _perfilRepo.Update(perfil);

        return nuevaUrl;
    }
}
