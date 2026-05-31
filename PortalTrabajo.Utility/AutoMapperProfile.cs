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
using PortalTrabajo.DTO.Postulaciones;
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
                .ForMember(destino => destino.Idiomas, opt => opt.MapFrom(origen => origen.EstudianteIdiomas))
                .ForMember(destino => destino.Proyectos, opt => opt.MapFrom(origen => origen.ProyectosEstudiantes))
                .ForMember(destino => destino.CarreraNombre, opt => opt.MapFrom(origen => origen.Carrera.Nombre)); ;
            CreateMap<PerfilEstudianteUpdateDTO, PerfilesEstudiante>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Educacion, EducacionDTO>()
                .ForMember(destino => destino.GradoAcademicoNombre, opt => opt.MapFrom(origen => origen.GradoAcademico.Nombre));
            CreateMap<ExperienciasLaborale, ExperienciaLaboralDTO>().ReverseMap();
            CreateMap<EstudianteHabilidade, EstudianteHabilidadDTO>()
                .ForMember(destino => destino.NombreHabilidad, opt => opt.MapFrom(origen => origen.Habilidad.Nombre));
            CreateMap<EstudianteIdioma, EstudianteIdiomaDTO>()
                .ForMember(destino => destino.NivelNombre, opt => opt.MapFrom(origen => origen.Nivel.Nombre));
            CreateMap<ProyectosEstudiante, ProyectoEstudianteDTO>().ReverseMap();
            #endregion
            #region Empresas
            CreateMap<ContactosEmpresa, ContactoEmpresaDTO>().ReverseMap();
            CreateMap<Empresa, EmpresaDTO>()
                .ForMember(dest => dest.Sector, opt => opt.MapFrom(src => src.SectorNavigation != null ? src.SectorNavigation.Nombre : null))
                .ForMember(dest => dest.Contacto, opt => opt.MapFrom(src => src.ContactosEmpresa))
                .ReverseMap()
                .ForMember(dest => dest.ContactosEmpresa, opt => opt.MapFrom(src => src.Contacto));
            CreateMap<EmpresaCreateDTO, Empresa>()
                .ForMember(dest => dest.UsuarioId, opt => opt.Ignore());
            CreateMap<EmpresaCreateDTO, Usuario>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.RolId, opt => opt.Ignore())
                .ForMember(dest => dest.Activo, opt => opt.Ignore());
            CreateMap<EmpresaUpdateDTO, Empresa>()
                .ForMember(dest => dest.ContactosEmpresa, opt => opt.Ignore());
            #endregion
            #region Ofertas Laborales
            CreateMap<OfertasLaborale, OfertaLaboralDTO>()
                .ForMember(destino => destino.EmpresaNombre, opt => opt.MapFrom(origen => origen.Empresa.NombreComercial))
                .ForMember(destino => destino.EmpresaLogoUrl, opt => opt.MapFrom(origen => origen.Empresa.LogoUrl))
                .ForMember(destino => destino.ModalidadNombre, opt => opt.MapFrom(origen => origen.Modalidad.Nombre))
                .ForMember(destino => destino.LicenciaNombre, opt => opt.MapFrom(origen => origen.Licencia != null ? origen.Licencia.Nombre : null))
                .ForMember(destino => destino.TipoContratoNombre, opt => opt.MapFrom(origen => origen.TipoContrato != null ? origen.TipoContrato.Nombre : null))
                .ForMember(destino => destino.DistritoNombre, opt => opt.MapFrom(origen => origen.Distrito != null ? origen.Distrito.Nombre : null))
                .ForMember(destino => destino.GeneroNombre, opt => opt.MapFrom(origen => origen.Genero != null ? origen.Genero.Nombre : null))
                .ForMember(destino => destino.Carreras, opt => opt.MapFrom(origen => origen.Carreras.Select(c => c.Nombre).ToList()))
                .ForMember(destino => destino.Habilidades, opt => opt.MapFrom(origen => origen.OfertaHabilidades));
            CreateMap<OfertaHabilidade, OfertaHabilidadDTO>()
                .ForMember(destino => destino.NombreHabilidad, opt => opt.MapFrom(origen => origen.Habilidad.Nombre))
                .ForMember(destino => destino.EsObligatorio, opt => opt.MapFrom(origen => origen.EsObligatorio ?? true));
            CreateMap<OfertaLaboralCreateDTO, OfertasLaborale>();
            CreateMap<OfertaLaboralUpdateDTO, OfertasLaborale>();
            #endregion
            #region Postulaciones
            CreateMap<Postulacione, PostulacionDTO>()
                .ForMember(dest => dest.OfertaTitulo, opt => opt.MapFrom(src => src.Oferta.Titulo))
                .ForMember(dest => dest.EmpresaNombre, opt => opt.MapFrom(src => src.Oferta.Empresa.NombreComercial))
                .ForMember(dest => dest.EstudianteNombreCompleto, opt => opt.MapFrom(src => src.Perfil.Nombres + " " + src.Perfil.Apellidos))
                .ForMember(dest => dest.EstadoNombre, opt => opt.MapFrom(src => src.Estado.Nombre))
                .ForMember(dest => dest.EstudianteFotoUrl, opt => opt.MapFrom(src => src.Perfil.FotoUrl))
                .ForMember(dest => dest.EmpresaLogoUrl, opt => opt.MapFrom(src => src.Oferta.Empresa.LogoUrl));
            #endregion
            #region Catalogos
            CreateMap<CatRole, CatalogDTO>().ReverseMap();
            CreateMap<CatCarrera, CatalogDTO>().ReverseMap();
            CreateMap<CatModalidade, CatalogDTO>().ReverseMap();
            CreateMap<CatGradosAcademico, CatalogDTO>().ReverseMap();
            CreateMap<CatNivelesIdioma, CatalogDTO>().ReverseMap();
            CreateMap<Habilidade, CatalogDTO>().ReverseMap();
            CreateMap<CatEstadosPostulacion, CatalogDTO>().ReverseMap();
            CreateMap<CatDepartamento, CatalogDTO>().ReverseMap();
            CreateMap<CatMunicipio, CatalogDTO>().ReverseMap();
            CreateMap<CatTiposContrato, CatalogDTO>().ReverseMap();
            CreateMap<CatTiposLicencium, CatalogDTO>().ReverseMap();
            CreateMap<CatGenero, CatalogDTO>().ReverseMap();
            CreateMap<CatDistrito, CatalogDTO>().ReverseMap();
            CreateMap<CatSector, CatalogDTO>().ReverseMap();
            #endregion
        }
    }
}
