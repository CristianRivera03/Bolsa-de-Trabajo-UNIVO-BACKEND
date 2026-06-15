using System;

namespace PortalTrabajo.DTO.Admin
{
    public class AuditLogDTO
    {
        public int Id { get; set; }
        public string NombreTabla { get; set; }
        public string Accion { get; set; }
        public string RegistroId { get; set; }
        public string ValoresAntiguos { get; set; }
        public string ValoresNuevos { get; set; }
        public DateTime Fecha { get; set; }
        public int? UsuarioId { get; set; }
    }
}
