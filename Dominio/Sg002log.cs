namespace Dominio;

public partial class Sg002log
{
    public int Sg002id { get; set; }

    public string? Sg002codigo { get; set; }

    public string Sg002mensaje { get; set; } = null!;

    public DateTime Sg002fecha { get; set; }

    public string? Sg002datos { get; set; }
}
