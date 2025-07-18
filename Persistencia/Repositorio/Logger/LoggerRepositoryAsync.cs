using Aplicacion.Dto;
using Aplicacion.Interfaces;
using Dominio;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Persistencia.Contexto;
using System;

namespace Persistencia.Repositorio.Logger
{
    public class LoggerRepositoryAsync : ILoggerRepositoryAsync
    {
        private readonly IOptions<LogsDto> _logsOptions;
        private readonly IServiceScopeFactory _scopecontextSql;

        public LoggerRepositoryAsync(IOptions<LogsDto> logsOptions, IServiceScopeFactory scopecontextSql)
        {
            _logsOptions = logsOptions;
            _scopecontextSql = scopecontextSql;
        }

        public async Task GuardarLog(Sg002log request)
        {
            //Leer configuracion
            if (_logsOptions.Value.TipoLog.Equals("Archivo"))
                await GuardarLogArchivo(request);
            else
                await GuardarLogBaseDatos(request);
        }

        public async Task GuardarLogBaseDatos(Sg002log request)
        {
            try
            {
                using (var scope = _scopecontextSql.CreateScope())
                {
                    var _appDbContext = scope.ServiceProvider.GetRequiredService<DbContextSql>(); 
                    _appDbContext.Sg002log.Add(request);

                    await _appDbContext.SaveChangesAsync();
                }
            }
            catch
            {
            }
        }

        public async Task GuardarLogArchivo(Sg002log request)
        {
            try
            {
                var logDirectory = Path.Combine(AppContext.BaseDirectory, "Logs");
                if (!Directory.Exists(logDirectory))
                    Directory.CreateDirectory(logDirectory);

                var _logFilePath = Path.Combine(logDirectory, $"Log{DateTime.Now}");

                using (StreamWriter writer = new StreamWriter(_logFilePath, true))
                {
                    await writer.WriteLineAsync(JsonConvert.SerializeObject(request));
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
