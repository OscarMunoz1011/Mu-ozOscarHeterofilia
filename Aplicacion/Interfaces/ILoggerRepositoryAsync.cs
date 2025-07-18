using Dominio;

namespace Aplicacion.Interfaces
{
    public interface ILoggerRepositoryAsync
    {
        Task GuardarLog(Sg002log request);
    }
}
