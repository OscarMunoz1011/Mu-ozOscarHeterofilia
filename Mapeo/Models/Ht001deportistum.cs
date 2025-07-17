using System;
using System.Collections.Generic;

namespace Mapeo.Models;

public partial class Ht001deportistum
{
    public int Ht001id { get; set; }

    public string? Ht001nombre { get; set; }

    public string? Ht001apellido { get; set; }

    public string Cf001codigo { get; set; } = null!;

    public bool Ht001estado { get; set; }

    public virtual Cf001pai Cf001codigoNavigation { get; set; } = null!;

    public virtual ICollection<Ht004resultado> Ht004resultados { get; set; } = new List<Ht004resultado>();
}
