namespace Dominio;

public partial class Ht003modalidad
{
    public string Ht003codigo { get; set; } = null!;

    public string Ht003nombre { get; set; } = null!;

    public string Ht003descripcion { get; set; } = null!;

    public int Ht002id { get; set; }

    public bool Ht003estado { get; set; }

    public virtual Ht002tipoModalidad Ht002 { get; set; } = null!;

    public virtual ICollection<Ht004resultado> Ht004resultados { get; set; } = new List<Ht004resultado>();
}
