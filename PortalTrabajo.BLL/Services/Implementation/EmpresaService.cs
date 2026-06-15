using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly Microsoft.Extensions.DependencyInjection.IServiceScopeFactory _serviceScopeFactory;
        public EmpresaService(
            IGenericRepository<Empresa> empresaRepositorio,
            IGenericRepository<Usuario> usuarioRepositorio,
            IMapper mapper,
            PortalTrabajo.Utility.Interfaces.ICloudinaryUtility cloudinaryUtility,
            Microsoft.Extensions.DependencyInjection.IServiceScopeFactory serviceScopeFactory)
        {
            _empresaRepositorio = empresaRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
            _cloudinaryUtility = cloudinaryUtility;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public async Task<EmpresaDTO> CrearEmpresa(EmpresaCreateDTO modelo)
        {
            var nuevoUsuario = _mapper.Map<Usuario>(modelo);
            nuevoUsuario.PasswordHash = SecurityHelper.HashPassword(modelo.Password);
            nuevoUsuario.RolId = 3;
            nuevoUsuario.Activo = true; 
            
            var nuevaEmpresa = _mapper.Map<Empresa>(modelo);
            
            // Asignar datos de contacto de RRHH de forma obligatoria en el registro
            nuevaEmpresa.ContactosEmpresa = new ContactosEmpresa
            {
                NombreCompleto = modelo.ContactoNombre ?? "Sin Nombre",
                Cargo = "Encargado de RRHH",
                Dui = modelo.ContactoDui ?? "00000000-0",
                TelefonoMovil = modelo.ContactoTelefono ?? "0000-0000",
                CorreoContacto = modelo.Email,
                Activo = true,
                FechaCreacion = DateTime.Now
            };

            nuevoUsuario.Empresa = nuevaEmpresa;
            
            var usuarioCreado = await _usuarioRepositorio.Create(nuevoUsuario);
            if (usuarioCreado == null)
                throw new Exception("No se pudo completar el registro.");

            usuarioCreado.Activo = false;
            await _usuarioRepositorio.Update(usuarioCreado);

            // Notificar al admin
            NotificarAdminNuevaEmpresa(nuevaEmpresa, modelo);

            return _mapper.Map<EmpresaDTO>(usuarioCreado.Empresa);
        }

        private void NotificarAdminNuevaEmpresa(Empresa nuevaEmpresa, EmpresaCreateDTO modelo)
        {
            Task.Run(async () =>
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    try
                    {
                        var repo = scope.ServiceProvider.GetRequiredService<IGenericRepository<Usuario>>();
                        var emailService = scope.ServiceProvider.GetRequiredService<PortalTrabajo.BLL.Services.Contract.IEmailService>();
                        var admin = await repo.Get(u => u.RolId == 1 && u.Activo == true);
                        if (admin != null && !string.IsNullOrEmpty(admin.Email))
                        {
                            string asunto = "Nueva Empresa Pendiente de Aprobación";
                            string cuerpo = $@"
                        <p>La empresa <strong>{nuevaEmpresa.NombreComercial}</strong> se ha registrado y está esperando tu aprobación.</p>
                        <h3>Datos de la Empresa:</h3>
                        <ul>
                            <li><strong>Razón Social:</strong> {modelo.RazonSocial}</li>
                            <li><strong>NIT:</strong> {modelo.Nit}</li>
                        </ul>
                        <h3>Datos del Encargado de RRHH:</h3>
                        <ul>
                            <li><strong>Nombre Completo:</strong> {modelo.ContactoNombre}</li>
                            <li><strong>Teléfono:</strong> {modelo.ContactoTelefono}</li>
                            <li><strong>DUI:</strong> {modelo.ContactoDui}</li>
                            <li><strong>Correo:</strong> {modelo.Email}</li>
                        </ul>
                        <p>Por favor, ingresa al panel de administración para revisar sus datos y activarla.</p>
                        <p><a href='#' class='btn'>Ir al Panel de Administración</a></p>";

                            string htmlFinal = PortalTrabajo.Utility.EmailTemplateBuilder.BuildTemplate("Nueva Solicitud de Empresa", cuerpo);
                            await emailService.EnviarCorreoAsync(admin.Email, asunto, htmlFinal);
                        }
                    }
                    catch { }
                }
            });
        }
        public async Task<EmpresaDTO> ObtenerMiEmpresaAsync(int usuarioId)
        {
            var query =  _empresaRepositorio.Query(e => e.UsuarioId == usuarioId);
            var empresa = await query
                .Include(e => e.ContactosEmpresa)
                .Include(e => e.Sector)
                .Include(e => e.Distrito)
                    .ThenInclude(d => d.Municipio)
                .FirstOrDefaultAsync();
            if (empresa == null) throw new Exception("Empresa no encontrada.");
            return _mapper.Map<EmpresaDTO>(empresa);
        }
        public async Task<EmpresaDTO> ActualizarEmpresaAsync(int usuarioId, EmpresaUpdateDTO dto)
        {
            var query = _empresaRepositorio.Query(e => e.UsuarioId == usuarioId);
            var empresa = await query
                .Include(e => e.ContactosEmpresa)
                .Include(e => e.Sector)
                .Include(e => e.Distrito)
                    .ThenInclude(d => d.Municipio)
                .FirstOrDefaultAsync();
            if (empresa == null) throw new Exception("Empresa no encontrada.");

            var logoUrlBackup = empresa.LogoUrl;
            _mapper.Map(dto, empresa);
            empresa.LogoUrl = logoUrlBackup; // Evitar que el DTO con LogoUrl null lo sobreescriba

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

        public async Task<EmpresaDTO> ObtenerEmpresaPorIdAsync(int empresaId)
        {
            var query = _empresaRepositorio.Query(e => e.Id == empresaId);
            var empresa = await query
                .Include(e => e.Sector)
                .Include(e => e.Distrito)
                    .ThenInclude(d => d.Municipio)
                        .ThenInclude(m => m.Departamento)
                // NO incluimos ContactosEmpresa para proteger privacidad
                .FirstOrDefaultAsync();

            if (empresa == null) throw new Exception("Empresa no encontrada.");

            return _mapper.Map<EmpresaDTO>(empresa);
        }
    }
}
