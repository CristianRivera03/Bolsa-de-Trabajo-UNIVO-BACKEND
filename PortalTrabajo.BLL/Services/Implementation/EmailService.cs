using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.Model;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
namespace PortalTrabajo.BLL.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly ConfiguracionCorreo _config;
        private readonly ILogger<EmailService> _logger;
        public EmailService(IOptions<ConfiguracionCorreo> config, ILogger<EmailService> logger)
        {
            _config = config.Value;
            _logger = logger;
        }
        public async Task EnviarCorreoAsync(string destinatario, string asunto, string cuerpoHtml)
        {
            if (string.IsNullOrWhiteSpace(_config.ServidorSmtp))
            {
                throw new InvalidOperationException("La configuración del servidor SMTP está incompleta (ServidorSmtp no especificado).");
            }
            if (string.IsNullOrWhiteSpace(_config.CorreoRemitente))
            {
                throw new InvalidOperationException("La dirección de correo del remitente está vacía.");
            }
            if (string.IsNullOrWhiteSpace(_config.ClaveApp))
            {
                throw new InvalidOperationException("La contraseña de aplicación (ClaveApp) está vacía.");
            }
            if (string.IsNullOrWhiteSpace(destinatario))
            {
                throw new ArgumentException("La dirección de correo del destinatario está vacía.");
            }
            if (!MailAddress.TryCreate(_config.CorreoRemitente, out _))
            {
                throw new FormatException($"La dirección de correo del remitente '{_config.CorreoRemitente}' no tiene un formato válido.");
            }
            if (!MailAddress.TryCreate(destinatario, out _))
            {
                throw new FormatException($"La dirección de correo de destino '{destinatario}' no tiene un formato válido.");
            }
            try
            {
                _logger.LogInformation("Enviando correo. Remitente: '{Remitente}' (Largo: {LargoRemitente}), Destinatario: '{Destinatario}' (Largo: {LargoDestinatario})", 
                    _config.CorreoRemitente, _config.CorreoRemitente.Length, destinatario, destinatario.Length);
                using (var client = new SmtpClient(_config.ServidorSmtp, _config.Puerto))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(_config.CorreoRemitente, _config.ClaveApp);
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_config.CorreoRemitente, _config.NombreRemitente),
                        Subject = asunto,
                        Body = cuerpoHtml,
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(destinatario);
                    await client.SendMailAsync(mailMessage);
                    _logger.LogInformation("Correo enviado exitosamente a {Destinatario} con asunto '{Asunto}'", destinatario, asunto);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar correo a {Destinatario}", destinatario);
                throw; 
            }
        }
    }
}
