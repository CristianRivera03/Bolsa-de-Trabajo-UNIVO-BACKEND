using System.Threading.Tasks;
namespace PortalTrabajo.BLL.Services.Contract
{
    public interface IEmailService
    {
        Task EnviarCorreoAsync(string destinatario, string asunto, string cuerpoHtml);
    }
}
