using PortalTrabajo.DTO.PerfilesEstudiante;
using PortalTrabajo.Utility.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortalTrabajo.Utility
{
    public class CvGeneratorService : ICvGeneratorService
    {
        public CvGeneratorService()
        {
            // Obligatorio para usar la versión gratuita
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public byte[] GenerarCvBasico(PerfilEstudianteDTO perfil)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    // ENCABEZADO BASICO
                    page.Header().Text($"{perfil.Nombres} {perfil.Apellidos}")
                        .SemiBold().FontSize(24).FontColor(Colors.Red.Darken4);

                    // CUERPO BASICO
                    page.Content().PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(10);

                            x.Item().Text("Sobre Mí").SemiBold().FontSize(14);
                            x.Item().Text(perfil.SobreMi ?? "Sin descripción.");

                            x.Item().PaddingTop(10).Text("Experiencia Laboral").SemiBold().FontSize(14);

                            foreach (var exp in perfil.ExperienciasLaborales)
                            {
                                x.Item().Text($"• {exp.Cargo} en {exp.Empresa} ({exp.FechaInicio:yyyy} - {(exp.FechaFin.HasValue ? exp.FechaFin.Value.ToString("yyyy") : "Actual")})");
                            }
                        });

                    // PIE DE PÁGINA BASICO
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Página ");
                        x.CurrentPageNumber();
                    });
                });
            });

            // Retorna los bytes del PDF
            return document.GeneratePdf();
        }
    }
}
