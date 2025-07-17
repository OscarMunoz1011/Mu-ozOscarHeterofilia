using System;
using System.Collections.Generic;

namespace Mapeo.Models;

public partial class Ht004resultado
{
    public int Ht004id { get; set; }

    public int Ht001id { get; set; }

    public string? Ht003codigo { get; set; }

    public int Ht004intento { get; set; }

    public int Ht004puntaje { get; set; }

    public virtual Ht001deportistum Ht001 { get; set; } = null!;

    public virtual Ht003modalidad? Ht003codigoNavigation { get; set; }
}
