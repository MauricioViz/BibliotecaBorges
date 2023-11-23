using System;
using System.Collections.Generic;

namespace BiblioAPI.Models;

public partial class TbLibro
{
    public string IdLibro { get; set; } = null!;

    public string TituloLibro { get; set; } = null!;

    public string GeneroLibro { get; set; } = null!;

    public string? FecPublic { get; set; }

    public string EstLibro { get; set; } = null!;

    public virtual ICollection<TbAutorLibro> TbAutorLibros { get; set; } = new List<TbAutorLibro>();

    public virtual ICollection<TbEditorialLibro> TbEditorialLibros { get; set; } = new List<TbEditorialLibro>();

    public virtual ICollection<TbPrestamoLibro> TbPrestamoLibros { get; set; } = new List<TbPrestamoLibro>();
}
