using Microsoft.AspNetCore.Http;
namespace PortalTrabajo.DTO.Shared
{
    public class CambiarImagenDTO
    {
        public IFormFile Archivo { get; set; }
    }
}
