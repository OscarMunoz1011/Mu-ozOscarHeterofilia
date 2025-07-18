namespace Aplicacion.Dto.Request
{
    public class RegistrarUsuarioRequest
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Usuario { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
