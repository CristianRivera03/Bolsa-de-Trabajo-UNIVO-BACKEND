using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PortalTrabajo.BLL.Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortalTrabajo.BLL.BackgroundServices
{
    public class OfertasExpiracionService : BackgroundService
    {

        private readonly IServiceProvider _serviceProvider;

        public OfertasExpiracionService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var ofertaService = scope.ServiceProvider.GetRequiredService<IOfertaLaboralService>();

                    await ofertaService.DesactivarOfertasExpiradas();
                }
                Task.Delay(TimeSpan.FromHours(1), stoppingToken).Wait();
            }
        }
    }
}
