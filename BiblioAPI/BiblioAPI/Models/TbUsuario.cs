using System;
using System.Collections.Generic;

namespace BiblioAPI.Models;

public partial class TbUsuario
{
    public string IdUsuario { get; set; } = null!;

    public string NomUsuario { get; set; } = null!;

    public string? TlfUsuario { get; set; }

    public string CorreoUsuario { get; set; } = null!;

    public string TipoUsuario { get; set; } = null!;

    public string PassUsuario { get; set; } = null!;

    public virtual ICollection<TbPrestamo> TbPrestamos { get; set; } = new List<TbPrestamo>();
}
