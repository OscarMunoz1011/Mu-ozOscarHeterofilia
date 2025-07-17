using System;
using System.Collections.Generic;

namespace Mapeo.Models;

public partial class Cf001pai
{
    public string Cf001codigo { get; set; } = null!;

    public string Cf001nombre { get; set; } = null!;

    public string? Cf001descripcion { get; set; }

    public bool Cf001estado { get; set; }

    public virtual ICollection<Ht001deportistum> Ht001deportista { get; set; } = new List<Ht001deportistum>();
}
