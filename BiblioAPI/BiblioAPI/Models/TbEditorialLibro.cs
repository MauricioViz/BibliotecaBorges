using System;
using System.Collections.Generic;

namespace BiblioAPI.Models;

public partial class TbEditorialLibro
{
    public string IdRel { get; set; } = null!;

    public string IdEditorial { get; set; } = null!;

    public string IdLibro { get; set; } = null!;

    public virtual TbEditorial IdEditorialNavigation { get; set; } = null!;

    public virtual TbLibro IdLibroNavigation { get; set; } = null!;
}
