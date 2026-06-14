using System;
using System.Threading.Tasks;

namespace PortalTrabajo.BLL.Services.Contract
{
    public interface IReportesService
    {
        Task<byte[]> GenerarReporteCandidatosExcelAsync(
            DateTime? fechaInicio, DateTime? fechaFin,
            int? carreraId, int? gradoAcademicoId,
            bool? estado, int? departamentoId);

        Task<byte[]> GenerarReporteEmpresasExcelAsync(
            DateTime? fechaInicio, DateTime? fechaFin,
            int? sectorId);
    }
}
