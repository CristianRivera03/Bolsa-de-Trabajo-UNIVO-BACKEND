using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortalTrabajo.Model;
using PortalTrabajo.DTO.Auth;
using PortalTrabajo.DTO.Usuarios;
using PortalTrabajo.DTO.Alumnos;
using PortalTrabajo.DTO.PerfilesEstudiante;
using PortalTrabajo.DTO.Empresas;
using PortalTrabajo.DTO.OfertasLaborales;
using PortalTrabajo.DTO.Catalogos;

namespace PortalTrabajo.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Alumnos (Mock DB)
            CreateMap<AlumnosActivo, AlumnoActivoDTO>();
            #endregion

            #region Usuarios & Auth
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino => destino.RolName, opt => opt.MapFrom(origen => origen.Rol.Nombre));

            CreateMap<UsuarioCreateDTO, Usuario>();

            CreateMap<Usuario, SessionDTO>()
                .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.Id))
                .ForMember(destino => destino.RolName, opt => opt.MapFrom(origen => origen.Rol.Nombre))
                .ForMember(destino => destino.NombreCompleto, opt => opt.MapFrom(origen => 
                    origen.PerfilesEstudiante != null ? origen.PerfilesEstudiante.Nombres + " " + origen.PerfilesEstudiante.Apellidos : 
                    (origen.Empresa != null ? origen.Empresa.NombreComercial : origen.Email)));
            #endregion

            #region Perfiles Estudiante
            CreateMap<PerfilesEstudiante, PerfilEstudianteDTO>()
                .ForMember(destino => destino.Educaciones, opt => opt.MapFrom(origen => origen.Educacions))
                .ForMember(destino => destino.Habilidades, opt => opt.MapFrom(origen => origen.EstudianteHabilidades))
                .ForMember(destino => destino.Idiomas, opt => opt.MapFrom(origen => origen.EstudianteIdiomas));

            CreateMap<PerfilEstudianteUpdateDTO, PerfilesEstudiante>();

            CreateMap<Educacion, EducacionDTO>()
                .ForMember(destino => destino.GradoAcademicoNombre, opt => opt.MapFrom(origen => origen.GradoAcademico.Nombre));

            CreateMap<ExperienciasLaborale, ExperienciaLaboralDTO>().ReverseMap();

            CreateMap<EstudianteHabilidade, EstudianteHabilidadDTO>()
                .ForMember(destino => destino.NombreHabilidad, opt => opt.MapFrom(origen => origen.Habilidad.Nombre));

            CreateMap<EstudianteIdioma, EstudianteIdiomaDTO>()
                .ForMember(destino => destino.NivelNombre, opt => opt.MapFrom(origen => origen.Nivel.Nombre));
            #endregion

            #region Empresas
            CreateMap<Empresa, EmpresaDTO>().ReverseMap();

            //Dto a empresas
            CreateMap<EmpresaCreateDTO, Empresa>()
                .ForMember(dest => dest.UsuarioId, opt => opt.Ignore());

            //Dto a usuarios

            CreateMap<EmpresaCreateDTO, Usuario>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.RolId, opt => opt.Ignore())
                .ForMember(dest => dest.Activo, opt => opt.Ignore());

            CreateMap<EmpresaUpdateDTO, Empresa>();
            #endregion

            #region Ofertas Laborales
            CreateMap<OfertasLaborale, OfertaLaboralDTO>()
                .ForMember(destino => destino.EmpresaNombre, opt => opt.MapFrom(origen => origen.Empresa.NombreComercial))
                .ForMember(destino => destino.EmpresaLogoUrl, opt => opt.MapFrom(origen => origen.Empresa.LogoUrl))
                .ForMember(destino => destino.ModalidadNombre, opt => opt.MapFrom(origen => origen.Modalidad.Nombre));

            CreateMap<OfertaLaboralCreateDTO, OfertasLaborale>();
            CreateMap<OfertaLaboralUpdateDTO, OfertasLaborale>();
            #endregion

            #region Catalogos
            CreateMap<CatRole, CatalogDTO>().ReverseMap();
            CreateMap<CatModalidade, CatalogDTO>().ReverseMap();
            CreateMap<CatGradosAcademico, CatalogDTO>().ReverseMap();
            CreateMap<CatNivelesIdioma, CatalogDTO>().ReverseMap();
            CreateMap<Habilidade, CatalogDTO>().ReverseMap();
            CreateMap<CatEstadosPostulacion, CatalogDTO>().ReverseMap();
            #endregion
        }
    }
}
