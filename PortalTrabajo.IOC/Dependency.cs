using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortalTrabajo.Model;
using PortalTrabajo.DAL.Repositories.Implementation;
using PortalTrabajo.DAL.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using PortalTrabajo.Utility;
using PortalTrabajo.BLL.BackgroundServices;
using PortalTrabajo.DAL.DBContext;
using PortalTrabajo.Utility.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
namespace PortalTrabajo.IOC
{
    public static class Dependency
    {
        public static void DependencyInjection(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<PortalTrabajoDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("connectionDB")
             ));

            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.Configure<ConfiguracionCorreo>(configuration.GetSection("ConfiguracionCorreo"));
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Key);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<PortalTrabajo.BLL.Services.Contract.IAlumnoService, PortalTrabajo.BLL.Services.Implementation.AlumnoService>();
            services.AddScoped<PortalTrabajo.BLL.Services.Contract.IOfertaLaboralService, PortalTrabajo.BLL.Services.Implementation.OfertaLaboralService>();
            services.AddScoped<PortalTrabajo.BLL.Services.Contract.IAuthService, PortalTrabajo.BLL.Services.Implementation.AuthService>();
            services.AddScoped<PortalTrabajo.BLL.Services.Contract.IEmpresaService, PortalTrabajo.BLL.Services.Implementation.EmpresaService>();
            services.AddScoped<PortalTrabajo.BLL.Services.Contract.ICatalogoService, PortalTrabajo.BLL.Services.Implementation.CatalogoService>();
            services.AddScoped<PortalTrabajo.BLL.Services.Contract.IPerfilEstudianteService, PortalTrabajo.BLL.Services.Implementation.PerfilEstudianteService>();
            services.AddScoped<PortalTrabajo.BLL.Services.Contract.IPostulacionService, PortalTrabajo.BLL.Services.Implementation.PostulacionService>();
            services.AddScoped<PortalTrabajo.BLL.Services.Contract.IExperienciaLaboralService, PortalTrabajo.BLL.Services.Implementation.ExperienciaLaboralService>();
            services.AddScoped<PortalTrabajo.BLL.Services.Contract.IProyectosService, PortalTrabajo.BLL.Services.Implementation.ProyectosService>();
            services.AddScoped<PortalTrabajo.BLL.Services.Contract.IEstudianteIdiomaService, PortalTrabajo.BLL.Services.Implementation.EstudianteIdiomaService>();
            services.AddScoped<PortalTrabajo.BLL.Services.Contract.IEducacionService, PortalTrabajo.BLL.Services.Implementation.EducacionService>();
            services.AddScoped<PortalTrabajo.BLL.Services.Contract.IEstudianteHabilidadService, PortalTrabajo.BLL.Services.Implementation.EstudianteHabilidadService>();
            services.AddScoped<PortalTrabajo.BLL.Services.Contract.IAdminService, PortalTrabajo.BLL.Services.Implementation.AdminService>();
            services.AddScoped<PortalTrabajo.BLL.Services.Contract.IEmailService, PortalTrabajo.BLL.Services.Implementation.EmailService>();
            services.AddHostedService<OfertasExpiracionService>();
            services.AddAutoMapper( cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            }, typeof(AutoMapperProfile));
            services.AddScoped<ICloudinaryUtility, CloudinaryUtility>();
            services.AddScoped<IJwtUtility, JwtUtility>();
            services.AddScoped<ICvGeneratorService, CvGeneratorService>();
        }
    }
}
