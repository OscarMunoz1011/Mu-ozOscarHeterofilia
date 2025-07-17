namespace Aplicacion.Dto.Response
{
    public class ResultadosHeterofiliaResponse
    {
        public string Pais { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int Arranque { get; set; }
        public int Envion { get; set; }
        public int TotalPeso { get; set; }
    }
}
