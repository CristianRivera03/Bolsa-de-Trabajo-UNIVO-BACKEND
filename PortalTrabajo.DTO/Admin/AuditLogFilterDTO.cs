using System;

namespace PortalTrabajo.DTO.Admin
{
    public class AuditLogFilterDTO
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? Tabla { get; set; }
        public string? Accion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
