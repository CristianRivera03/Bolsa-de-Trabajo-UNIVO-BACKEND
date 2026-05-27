using System;
using System.Collections.Generic;
namespace PortalTrabajo.Model;
public partial class ContactosEmpresa
{
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public string NombreCompleto { get; set; } = null!;
    public string Cargo { get; set; } = null!;
    public string Dui { get; set; } = null!;
    public string TelefonoMovil { get; set; } = null!;
    public string CorreoContacto { get; set; } = null!;
    public virtual Empresa Empresa { get; set; } = null!;
}
