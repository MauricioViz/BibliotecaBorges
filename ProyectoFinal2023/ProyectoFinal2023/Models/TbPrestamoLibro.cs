using System;
using System.Collections.Generic;

namespace ProyectoFinal2023.Models;

public partial class TbPrestamoLibro
{
    public string IdRel { get; set; } = null!;

    public string IdPrestamo { get; set; } = null!;

    public string IdLibro { get; set; } = null!;

    public int Cantidad { get; set; }

    public virtual TbLibro IdLibroNavigation { get; set; } = null!;

    public virtual TbPrestamo IdPrestamoNavigation { get; set; } = null!;
}
