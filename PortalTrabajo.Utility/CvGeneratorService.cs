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
        private readonly string AzulUnivo = "#10406d";
        private readonly string AmarilloUnivo = "#f4af10";
        public CvGeneratorService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }
        public async Task<byte[]> GenerarCvUnivoAsync(PerfilEstudianteDTO perfil)
        {
            byte[] fotoBytes = null;
            if (!string.IsNullOrEmpty(perfil.FotoUrl))
            {
                string urlFoto = perfil.FotoUrl;
                if (urlFoto.Contains("cloudinary.com") && !urlFoto.Contains("r_max"))
                {
                    urlFoto = urlFoto.Replace("/upload/", "/upload/w_300,h_300,c_fill,r_max,f_png/");
                }
                using var httpClient = new HttpClient();
                try { fotoBytes = await httpClient.GetByteArrayAsync(urlFoto); }
                catch {  }
            }
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(0); 
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10).FontFamily(Fonts.Arial));
                    page.Content().Row(row =>
                    {
                        row.ConstantItem(220) 
                           .Background(AzulUnivo)
                           .Padding(20)
                           .Column(col =>
                           {
                               if (fotoBytes != null)
                               {
                                   col.Item().AlignCenter().Width(120).Height(120)
                                      .Image(fotoBytes).FitArea();
                               }
                               else
                               {
                                   col.Item().AlignCenter().Width(120).Height(120)
                                      .Background(Colors.Grey.Lighten2);
                               }
                               col.Item().PaddingTop(20);
                               TituloIzquierdo(col, "CONTACTO");
                               col.Item().PaddingTop(10).Text(perfil.Telefono).FontColor(Colors.White);
                               col.Item().PaddingTop(5).Text("crisriy2003@gmail.com").FontColor(Colors.White); 
                               col.Item().PaddingTop(5).Text(perfil.Direccion).FontColor(Colors.White);
                               if (!string.IsNullOrEmpty(perfil.EnlaceGitHub))
                                   col.Item().PaddingTop(5).Text(perfil.EnlaceGitHub.Replace("https://", "")).FontColor(Colors.White);
                               col.Item().PaddingTop(20);
                               TituloIzquierdo(col, "EDUCACIÓN");
                               foreach (var edu in perfil.Educaciones)
                               {
                                   col.Item().PaddingTop(10).Text(edu.TituloObtenido).FontColor(Colors.White).SemiBold();
                                   col.Item().Text(edu.Institucion).FontColor(Colors.Grey.Lighten2).FontSize(9);
                                   col.Item().Text($"{edu.FechaInicio:yyyy} - {(edu.FechaFin.HasValue ? edu.FechaFin.Value.ToString("yyyy") : "Actualidad")}").FontColor(Colors.Grey.Lighten2).FontSize(9);
                               }
                               col.Item().PaddingTop(20);
                               TituloIzquierdo(col, "HABILIDADES");
                               foreach (var hab in perfil.Habilidades)
                               {
                                   col.Item().PaddingTop(5).Text($"• {hab.NombreHabilidad}").FontColor(Colors.White);
                               }
                               col.Item().PaddingTop(20);
                               TituloIzquierdo(col, "IDIOMAS");
                               foreach (var idioma in perfil.Idiomas)
                               {
                                   col.Item().PaddingTop(5).Text($"{idioma.Idioma} - {idioma.NivelNombre}").FontColor(Colors.White);
                               }
                           });
                        row.RelativeItem() // Toma el espacio restante
                           .Background(Colors.White)
                           .Padding(30)
                           .Column(col =>
                           {
                               col.Item().Text($"{perfil.Nombres}").FontSize(30).FontColor(AzulUnivo).Bold();
                               col.Item().Text($"{perfil.Apellidos}").FontSize(30).FontColor(AzulUnivo).Bold();
                               col.Item().PaddingTop(5)
                                  .Text(perfil.CarreraNombre ?? "Profesional Universitario")
                                  .FontSize(14).FontColor(Colors.Grey.Medium).LetterSpacing(0.05f);
                               col.Item().PaddingTop(20);
                               col.Item().Text(perfil.SobreMi).FontColor(Colors.Grey.Darken3).LineHeight(1.5f);
                               col.Item().PaddingTop(25);
                               TituloDerecho(col, "EXPERIENCIA LABORAL");
                               foreach (var exp in perfil.ExperienciasLaborales)
                               {
                                   col.Item().PaddingTop(15).Row(r =>
                                   {
                                       r.RelativeItem().Text(exp.Cargo).SemiBold().FontSize(12).FontColor(AzulUnivo);
                                       r.ConstantItem(100).AlignRight().Text($"{exp.FechaInicio:yyyy} - {(exp.FechaFin.HasValue ? exp.FechaFin.Value.ToString("yyyy") : "Actual")}").FontSize(10).FontColor(Colors.Grey.Medium);
                                   });
                                   col.Item().Text(exp.Empresa).Italic().FontColor(Colors.Grey.Darken1);
                                   col.Item().PaddingTop(5).Text(exp.DescripcionPuesto).FontColor(Colors.Grey.Darken3);
                               }
                               if (perfil.Proyectos != null && perfil.Proyectos.Count > 0)
                               {
                                   col.Item().PaddingTop(25);
                                   TituloDerecho(col, "PROYECTOS DESTACADOS");
                                   foreach (var proy in perfil.Proyectos)
                                   {
                                       col.Item().PaddingTop(10).Text(proy.Nombre).SemiBold().FontColor(AzulUnivo);
                                       col.Item().Text(proy.TecnologiasUsadas).FontSize(9).FontColor(AmarilloUnivo).Bold();
                                       col.Item().Text(proy.Descripcion).FontColor(Colors.Grey.Darken3);
                                   }
                               }
                           });
                    });
                });
            });
            return document.GeneratePdf();
        }
        private void TituloIzquierdo(ColumnDescriptor col, string texto)
        {
            col.Item().Background(AmarilloUnivo)
               .PaddingVertical(5).PaddingHorizontal(10)
               .AlignCenter()
               .Text(texto).FontColor(AzulUnivo).SemiBold().FontSize(12);
        }
        private void TituloDerecho(ColumnDescriptor col, string texto)
        {
            col.Item().BorderBottom(1).BorderColor(AzulUnivo)
               .PaddingBottom(5)
               .Text(texto).FontColor(AzulUnivo).Bold().FontSize(14);
        }
    }
}
