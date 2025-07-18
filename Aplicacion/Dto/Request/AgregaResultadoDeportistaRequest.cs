namespace Aplicacion.Dto.Request
{
    public class AgregaResultadoDeportistaRequest
    {
        public int IdDeportista { get; set; }
        public string CodigoModalidad { get; set; } = null!;
        public int Puntaje { get; set; }
    }
}
