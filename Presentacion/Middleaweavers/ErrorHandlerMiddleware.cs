﻿using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using Dominio;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Presentacion.Middleaweavers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerRepositoryAsync _logger;
        private string? body;

        public ErrorHandlerMiddleware(RequestDelegate next, ILoggerRepositoryAsync logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                context.Request.EnableBuffering();
                var reader = new StreamReader(context.Request.Body);
                body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;

                if (string.IsNullOrEmpty(body) && context.Request.QueryString.HasValue)
                {
                    body = context.Request.QueryString.Value;
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                var responseModel = new Response<string>() { Succeeded = false, Message = ex.Message };
                response.ContentType = "application/json";
                await _logger.GuardarLog(new Sg002log
                {
                    Sg002codigo = ex.HResult.ToString(),
                    Sg002mensaje = ex.Message,
                    Sg002datos = body,
                    Sg002fecha = DateTime.Now
                });
                await response.WriteAsync(JsonConvert.SerializeObject(responseModel));
            }
        }
    }
}