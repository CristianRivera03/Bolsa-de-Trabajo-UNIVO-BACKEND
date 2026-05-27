using AutoMapper;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DAL.Repositories.Contract;
using PortalTrabajo.DTO.Empresas;
using PortalTrabajo.Model;
using PortalTrabajo.Utility;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace PortalTrabajo.BLL.Services.Implementation
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IGenericRepository<Empresa> _empresaRepositorio;
        private readonly IGenericRepository<Usuario> _usuarioRepositorio;
        private readonly IMapper _mapper;
        private readonly PortalTrabajo.Utility.Interfaces.ICloudinaryUtility _cloudinaryUtility;
        public EmpresaService(
            IGenericRepository<Empresa> empresaRepositorio,
            IGenericRepository<Usuario> usuarioRepositorio,
            IMapper mapper,
            PortalTrabajo.Utility.Interfaces.ICloudinaryUtility cloudinaryUtility)
        {
            _empresaRepositorio = empresaRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
            _cloudinaryUtility = cloudinaryUtility;
        }
        public async Task<EmpresaDTO> CrearEmpresa(EmpresaCreateDTO modelo)
        {
            var nuevoUsuario = _mapper.Map<Usuario>(modelo);
            nuevoUsuario.PasswordHash = SecurityHelper.HashPassword(modelo.Password);
            nuevoUsuario.RolId = 3;
            nuevoUsuario.Activo = true;
            var nuevaEmpresa = _mapper.Map<Empresa>(modelo);
            nuevoUsuario.Empresa = nuevaEmpresa;
            var usuarioCreado = await _usuarioRepositorio.Create(nuevoUsuario);
            if (usuarioCreado == null)
                throw new Exception("No se pudo completar el registro.");
            return _mapper.Map<EmpresaDTO>(usuarioCreado.Empresa);
        }
        public async Task<EmpresaDTO> ObtenerMiEmpresaAsync(int usuarioId)
        {
            var query =  _empresaRepositorio.Query(e => e.UsuarioId == usuarioId);
            var empresa = await query.Include(e => e.ContactosEmpresa).FirstOrDefaultAsync();
            if (empresa == null) throw new Exception("Empresa no encontrada.");
            return _mapper.Map<EmpresaDTO>(empresa);
        }
        public async Task<EmpresaDTO> ActualizarEmpresaAsync(int usuarioId, EmpresaUpdateDTO dto)
        {
            var query = _empresaRepositorio.Query(e => e.UsuarioId == usuarioId);
            var empresa = await query.Include(e => e.ContactosEmpresa).FirstOrDefaultAsync();
            if (empresa == null) throw new Exception("Empresa no encontrada.");
            _mapper.Map(dto, empresa);
            if (dto.Contacto != null)
            {
                if (empresa.ContactosEmpresa == null)
                {
                    empresa.ContactosEmpresa = _mapper.Map<ContactosEmpresa>(dto.Contacto);
                    empresa.ContactosEmpresa.EmpresaId = empresa.Id;
                }
                else
                {
                    var idActual = empresa.ContactosEmpresa.Id; 
                    _mapper.Map(dto.Contacto, empresa.ContactosEmpresa); 
                    empresa.ContactosEmpresa.Id = idActual; 
                    empresa.ContactosEmpresa.EmpresaId = empresa.Id; 
                }
            }
            await _empresaRepositorio.Update(empresa);
            return _mapper.Map<EmpresaDTO>(empresa);
        }
        public async Task<string> CambiarLogoAsync(int usuarioId, PortalTrabajo.DTO.Shared.CambiarImagenDTO dto)
        {
            var empresa = await _empresaRepositorio.Get(e => e.UsuarioId == usuarioId);
            if (empresa == null) throw new Exception("Empresa no encontrada.");
            if (dto.Archivo == null || dto.Archivo.Length == 0)
                throw new Exception("No se ha proporcionado ninguna imagen.");
            string nuevaUrl = await _cloudinaryUtility.SubirImagenAsync(dto.Archivo, "Empresas");
            if (string.IsNullOrEmpty(nuevaUrl))
                throw new Exception("Error al subir el logo a Cloudinary.");
            if (!string.IsNullOrEmpty(empresa.LogoUrl) && empresa.LogoUrl.Contains("cloudinary.com"))
            {
                var segments = new Uri(empresa.LogoUrl).Segments;
                var publicIdWithExtension = string.Join("", segments.Skip(segments.Length - 3));
                var publicId = System.IO.Path.ChangeExtension(publicIdWithExtension, null).Replace("/", "/");
                try
                {
                    var idToDestroy = publicIdWithExtension.Substring(0, publicIdWithExtension.LastIndexOf('.'));
                    await _cloudinaryUtility.EliminarImagenAsync(idToDestroy);
                }
                catch { }
            }
            empresa.LogoUrl = nuevaUrl;
            await _empresaRepositorio.Update(empresa);
            return nuevaUrl;
        }
    }
}
