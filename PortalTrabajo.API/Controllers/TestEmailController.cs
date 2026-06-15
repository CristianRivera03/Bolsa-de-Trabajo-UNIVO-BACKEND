using Microsoft.AspNetCore.Mvc;
using PortalTrabajo.BLL.Services.Contract;
using PortalTrabajo.API.Utility;
using System;
using System.Threading.Tasks;
namespace PortalTrabajo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestEmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public TestEmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpPost("send")]
        public async Task<IActionResult> TestSend([FromQuery] string destinatario)
        {
            var rsp = new Response<string>();
            try
            {
                var asunto = "Prueba de Configuración de Correo - Portal de Trabajo";
                var cuerpoHtml = @"
                    <html>
                    <body style='font-family: Arial, sans-serif;'>
                        <h2 style='color: #4f46e5;'>¡Conexión Exitosa!</h2>
                        <p>Este es un correo de prueba enviado desde la API de Portal de Trabajo.</p>
                        <p>Si has recibido este correo, tus credenciales de SMTP están configuradas correctamente.</p>
                    </body>
                    </html>";
                await _emailService.EnviarCorreoAsync(destinatario, asunto, cuerpoHtml);
                rsp.status = true;
                rsp.value = "Correo de prueba enviado con éxito.";
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = $"Error al enviar el correo: {ex.Message}";
                return BadRequest(rsp);
            }
        }
    }
}
