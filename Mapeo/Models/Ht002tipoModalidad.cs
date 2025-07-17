using System;
using System.Collections.Generic;

namespace Mapeo.Models;

public partial class Ht002tipoModalidad
{
    public int Ht002id { get; set; }

    public bool Ht002esEnvion { get; set; }

    public bool Ht002esArranque { get; set; }

    public virtual ICollection<Ht003modalidad> Ht003modalidads { get; set; } = new List<Ht003modalidad>();
}
