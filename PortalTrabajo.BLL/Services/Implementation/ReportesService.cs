using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.DAL.Repositories.Contract;
using PortalTrabajo.Model;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PortalTrabajo.BLL.Services.Implementation
{
    public class ReportesService : IReportesService
    {
        private readonly IGenericRepository<PerfilesEstudiante> _perfilRepository;
        private readonly IGenericRepository<Empresa> _empresaRepository;

        public ReportesService(IGenericRepository<PerfilesEstudiante> perfilRepository, IGenericRepository<Empresa> empresaRepository)
        {
            _perfilRepository = perfilRepository;
            _empresaRepository = empresaRepository;
        }

        public async Task<byte[]> GenerarReporteCandidatosExcelAsync(DateTime? fechaInicio, DateTime? fechaFin, int? carreraId, int? gradoAcademicoId, bool? estado, int? departamentoId)
        {
            var query = _perfilRepository.Query()
                .Include(p => p.Usuario)
                .Include(p => p.Carrera).Include(p => p.Distrito).ThenInclude(d => d.Municipio).ThenInclude(m => m.Departamento)
                .Include(p => p.ExperienciasLaborales)
                .Include(p => p.Educacions)
                .AsQueryable();

            if (fechaInicio.HasValue) query = query.Where(p => p.Usuario.FechaRegistro >= fechaInicio.Value);
            if (fechaFin.HasValue) query = query.Where(p => p.Usuario.FechaRegistro <= fechaFin.Value.AddDays(1).AddTicks(-1));
            if (carreraId.HasValue) query = query.Where(p => p.CarreraId == carreraId.Value);
            if (estado.HasValue) query = query.Where(p => p.Usuario.Activo == estado.Value);

            var perfiles = await query.ToListAsync();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Candidatos");

            int row = 1;
            // Headers
            var headers = new string[] { 
                "FECHA DE REGISTRO", "ESTADO", "NIVEL ACADÉMICO", "NOMBRE", "APELLIDO", 
                "CARNET", "GÉNERO", "EDAD", "CARRERA", "FACULTAD", "CORREO", "TELÉFONO", 
                "CELULAR", "DIRECCIÓN", "MUNICIPIO", "DEPARTAMENTO", 
                "EMPRESA", "CARGO", "DIRECCIÓN TRABAJO", "MUNICIPIO TRABAJO", 
                "DEPARTAMENTO TRABAJO", "SECTOR", "TELÉFONO TRABAJO" 
            };

            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cell(row, i + 1).Value = headers[i];
                worksheet.Cell(row, i + 1).Style.Font.Bold = true;
                worksheet.Cell(row, i + 1).Style.Fill.BackgroundColor = XLColor.LightBlue;
            }

            foreach (var p in perfiles)
            {
                row++;
                var expReciente = p.ExperienciasLaborales?.OrderByDescending(e => e.FechaInicio).FirstOrDefault();

                worksheet.Cell(row, 1).Value = p.Usuario?.FechaRegistro?.ToString("dd/MM/yyyy") ?? "";
                worksheet.Cell(row, 2).Value = (p.Usuario?.Activo ?? false) ? "Activo" : "Inactivo";
                worksheet.Cell(row, 3).Value = "";
                worksheet.Cell(row, 4).Value = p.Nombres;
                worksheet.Cell(row, 5).Value = p.Apellidos;
                worksheet.Cell(row, 6).Value = p.Carnet ?? ""; 
                worksheet.Cell(row, 7).Value = p.Genero ?? "";
                worksheet.Cell(row, 8).Value = p.FechaNacimiento.HasValue ? (DateTime.Now.Year - p.FechaNacimiento.Value.Year).ToString() : "";
                worksheet.Cell(row, 9).Value = p.Carrera?.Nombre ?? "";
                worksheet.Cell(row, 10).Value = p.Carrera?.Facultad ?? "";
                worksheet.Cell(row, 11).Value = p.Usuario?.Email ?? "";
                worksheet.Cell(row, 12).Value = p.Telefono ?? "";
                worksheet.Cell(row, 13).Value = p.Telefono ?? "";
                worksheet.Cell(row, 14).Value = p.Distrito?.Nombre ?? "";
                worksheet.Cell(row, 15).Value = p.Distrito?.Municipio?.Nombre ?? "";
                worksheet.Cell(row, 16).Value = p.Distrito?.Municipio?.Departamento?.Nombre ?? "";

                // Datos laborales
                if (expReciente != null)
                {
                    worksheet.Cell(row, 17).Value = expReciente.Empresa ?? "";
                    worksheet.Cell(row, 18).Value = expReciente.Cargo ?? "";
                    worksheet.Cell(row, 19).Value = ""; 
                    worksheet.Cell(row, 20).Value = "";
                    worksheet.Cell(row, 21).Value = "";
                    worksheet.Cell(row, 22).Value = "";
                    worksheet.Cell(row, 23).Value = ""; 
                }
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public async Task<byte[]> GenerarReporteEmpresasExcelAsync(DateTime? fechaInicio, DateTime? fechaFin, int? sectorId)
        {
            var query = _empresaRepository.Query()
                .Include(e => e.Usuario)
                .Include(e => e.Sector).Include(e => e.Distrito).ThenInclude(d => d.Municipio).ThenInclude(m => m.Departamento)
                .AsQueryable();

            if (fechaInicio.HasValue) query = query.Where(e => e.Usuario.FechaRegistro >= fechaInicio.Value);
            if (fechaFin.HasValue) query = query.Where(e => e.Usuario.FechaRegistro <= fechaFin.Value.AddDays(1).AddTicks(-1));
            if (sectorId.HasValue) query = query.Where(e => e.SectorId == sectorId.Value);

            var empresas = await query.ToListAsync();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Empresas");

            int row = 1;
            var headers = new string[] { 
                "FECHA REGISTRO", "ESTADO", "NOMBRE COMERCIAL", "RAZÓN SOCIAL", "NIT", 
                "SECTOR", "SITIO WEB", "TELÉFONO", "CORREO", "DIRECCIÓN", "MUNICIPIO", "DEPARTAMENTO" 
            };

            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cell(row, i + 1).Value = headers[i];
                worksheet.Cell(row, i + 1).Style.Font.Bold = true;
                worksheet.Cell(row, i + 1).Style.Fill.BackgroundColor = XLColor.LightBlue;
            }

            foreach (var e in empresas)
            {
                row++;
                worksheet.Cell(row, 1).Value = e.Usuario?.FechaRegistro?.ToString("dd/MM/yyyy") ?? "";
                worksheet.Cell(row, 2).Value = (e.Usuario?.Activo ?? false) ? "Aprobada" : "Pendiente";
                worksheet.Cell(row, 3).Value = e.NombreComercial ?? "";
                worksheet.Cell(row, 4).Value = e.RazonSocial ?? "";
                worksheet.Cell(row, 5).Value = e.Nit ?? "";
                worksheet.Cell(row, 6).Value = e.Sector?.Nombre ?? "";
                worksheet.Cell(row, 7).Value = e.SitioWeb ?? "";
                worksheet.Cell(row, 8).Value = e.TelefonoFijo ?? "";
                worksheet.Cell(row, 9).Value = e.Usuario?.Email ?? "";
                worksheet.Cell(row, 10).Value = e.Distrito?.Nombre ?? "";
                worksheet.Cell(row, 11).Value = e.Distrito?.Municipio?.Nombre ?? "";
                worksheet.Cell(row, 12).Value = e.Distrito?.Municipio?.Departamento?.Nombre ?? "";
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
