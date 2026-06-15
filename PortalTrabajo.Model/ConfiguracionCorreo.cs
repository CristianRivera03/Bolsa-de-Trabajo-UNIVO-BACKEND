using System;
namespace PortalTrabajo.Model
{
    public class ConfiguracionCorreo
{
        public string ServidorSmtp { get; set; } = string.Empty;
        public int Puerto { get; set; }
        public string CorreoRemitente { get; set; } = string.Empty;
        public string ClaveApp { get; set; } = string.Empty;
        public string NombreRemitente { get; set; } = string.Empty;
    }
}

