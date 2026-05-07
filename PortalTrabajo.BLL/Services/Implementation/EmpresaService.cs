using AutoMapper;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DAL.Repositories.Contract;
using PortalTrabajo.DTO.Empresas;
using PortalTrabajo.Model;
using PortalTrabajo.Utility;
using System;
using System.Threading.Tasks;

namespace PortalTrabajo.BLL.Services.Implementation
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IGenericRepository<Empresa> _empresaRepositorio;
        private readonly IGenericRepository<Usuario> _usuarioRepositorio;
        private readonly IMapper _mapper;

        public EmpresaService(IGenericRepository<Empresa> empresaRepositorio, IGenericRepository<Usuario> usuarioRepositorio, IMapper mapper)
        {
            _empresaRepositorio = empresaRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
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
    }
}
