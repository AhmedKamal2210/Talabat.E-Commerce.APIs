using System.Net;
using System.Text.Json;
using Talabat.E_Commerce1.HandleRespones;

namespace Talabat.E_Commerce1.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _Next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleWare(RequestDelegate next , ILogger<ExceptionMiddleWare> logger , IHostEnvironment environment )
        {
            _Next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _Next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var response = _environment.IsDevelopment()
                                                   ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                                                   : new ApiException((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
