using System;
using System.Collections.Generic;
namespace PortalTrabajo.Model;
public partial class Usuario
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public int RolId { get; set; }
    public DateTime? FechaRegistro { get; set; }
    public bool? Activo { get; set; }
    public virtual Empresa? Empresa { get; set; }
    public virtual PerfilesEstudiante? PerfilesEstudiante { get; set; }
    public virtual CatRole Rol { get; set; } = null!;
}
