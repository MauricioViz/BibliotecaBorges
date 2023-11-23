using System;
using System.Collections.Generic;

namespace BiblioAPI.Models;

public partial class TbEditorial
{
    public string IdEditorial { get; set; } = null!;

    public string NomEditorial { get; set; } = null!;

    public string DirEditorial { get; set; } = null!;

    public string TlfEditorial { get; set; } = null!;

    public virtual ICollection<TbEditorialLibro> TbEditorialLibros { get; set; } = new List<TbEditorialLibro>();
}
