using Aplicacion.Dto.Response;
using Aplicacion.Interfaces;
using Persistencia.Contexto;

namespace Persistencia.Repositorio
{
    public class ResultadoRepository : IResultadoRepository
    {
        private readonly DbContextSql _dbcontextSql;
        public ResultadoRepository(DbContextSql dbcontextSql) 
        {
            _dbcontextSql = dbcontextSql;   
        }

        public async Task<List<ResultadosHeterofiliaResponse>> DevuelveResultadosHeterofilia(int cantidadPaginas, int cantidadResultados)
        {
            List<ResultadosHeterofiliaResponse> response = new();

        }
    }
}
