using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
namespace PortalTrabajo.Utility.Interfaces
{
    public interface ICloudinaryUtility
    {
        Task<string> SubirImagenAsync(IFormFile archivo, string carpeta);
        Task<bool> EliminarImagenAsync(string publicId);
    }
}
