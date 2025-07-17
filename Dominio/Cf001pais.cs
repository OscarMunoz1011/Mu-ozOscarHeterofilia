namespace Dominio;

public partial class Cf001pais
{
    public string Cf001codigo { get; set; } = null!;

    public string Cf001nombre { get; set; } = null!;

    public string? Cf001descripcion { get; set; }

    public bool Cf001estado { get; set; }

    public virtual ICollection<Ht001deportista> Ht001deportista { get; set; } = new List<Ht001deportista>();
}
