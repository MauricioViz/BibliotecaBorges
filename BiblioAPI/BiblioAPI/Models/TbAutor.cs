using System;
using System.Collections.Generic;

namespace BiblioAPI.Models;

public partial class TbAutor
{
    public string IdAutor { get; set; } = null!;

    public string NomAutor { get; set; } = null!;

    public string Nacionalidad { get; set; } = null!;

    public string? FechaNacimiento { get; set; }

    public virtual ICollection<TbAutorLibro> TbAutorLibros { get; set; } = new List<TbAutorLibro>();
}
