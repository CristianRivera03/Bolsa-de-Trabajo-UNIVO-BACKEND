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

namespace PortalTrabajo.IOC
{
    public static class Dependency
    {
        public static void DependencyInjection(this IServiceCollection services , IConfiguration configuration)
        {
            // Register your dependencies here
            services.AddDbContext<PortalTrabajoDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("connectionDB")
             ));


            //Repos
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //Services
            services.AddScoped<PortalTrabajo.BLL.Services.Contract.IAlumnoService, PortalTrabajo.BLL.Services.Implementation.AlumnoService>();
            services.AddScoped<PortalTrabajo.BLL.Services.Contract.IOfertaLaboralService, PortalTrabajo.BLL.Services.Implementation.OfertaLaboralService>();
            services.AddScoped<PortalTrabajo.BLL.Services.Contract.IAuthService, PortalTrabajo.BLL.Services.Implementation.AuthService>();
            services.AddScoped<PortalTrabajo.BLL.Services.Contract.IEmpresaService, PortalTrabajo.BLL.Services.Implementation.EmpresaService>();
            services.AddScoped<PortalTrabajo.BLL.Services.Contract.ICatalogoService, PortalTrabajo.BLL.Services.Implementation.CatalogoService>();
            //Automapper
            services.AddAutoMapper( cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            }, typeof(AutoMapperProfile));


        }
    }
}
