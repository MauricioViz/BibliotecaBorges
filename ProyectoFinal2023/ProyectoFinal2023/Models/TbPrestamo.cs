using System;
using System.Collections.Generic;

namespace ProyectoFinal2023.Models;

public partial class TbPrestamo
{
    public string IdPrestamo { get; set; } = null!;

    public string IdUsuario { get; set; } = null!;

    public string FecPrestamo { get; set; } = null!;

    public string FecDevolucion { get; set; } = null!;

    public string EstPrestamo { get; set; } = null!;

    public double MultaPrestamo { get; set; }

    public virtual TbUsuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<TbPrestamoLibro> TbPrestamoLibros { get; set; } = new List<TbPrestamoLibro>();
}
