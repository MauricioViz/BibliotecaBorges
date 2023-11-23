using System;
using System.Collections.Generic;

namespace ProyectoFinal2023.Models;

public partial class TbAutorLibro
{
    public string IdRel { get; set; } = null!;

    public string IdAutor { get; set; } = null!;

    public string IdLibro { get; set; } = null!;

    public virtual TbAutor IdAutorNavigation { get; set; } = null!;

    public virtual TbLibro IdLibroNavigation { get; set; } = null!;
}
