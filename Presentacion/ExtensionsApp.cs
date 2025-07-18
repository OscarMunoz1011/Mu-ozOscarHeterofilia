using Presentacion.Middleaweavers;

namespace Presentacion
{
    public static class ExtensionesApp
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
