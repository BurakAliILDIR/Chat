using Chat.API.CQRS.Base;
using Newtonsoft.Json;

namespace Chat.API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                var error = new Dictionary<string, string>();

                error.Add("Message", ex.Message);

                context.Response.ContentType = "application/json";
                var response = new BaseResponse()
                {
                    Status = Result.Error,
                    Message = "Hata!",
                    Data = error
                };
                var json = JsonConvert.SerializeObject(response);
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(json);
            }
        }
    }

    // Extension method ile IApplicationBuilder altına custom methodumuzu eklenmesini sağlıyoruz.
    public static class ErrorMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}