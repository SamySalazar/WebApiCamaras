using Microsoft.Extensions.Logging;

namespace WebApiCamaras.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseHttpMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ResponseHttpMiddleware>();
        }
    }

    public class ResponseHttpMiddleware
    {
        private readonly RequestDelegate siguiente;
        private readonly ILogger<ResponseHttpMiddleware> logger;

        public ResponseHttpMiddleware(RequestDelegate siguiente, ILogger<ResponseHttpMiddleware> logger) 
        {
            this.siguiente = siguiente;
            this.logger = logger;
        }

        // Invoke o InvokeAsync
        public async Task InvokeAsync(HttpContext context)
        {
            //Use me permite agregar mi propio proceso sin afectar a los demas a diferencia de Run
            using (var ms = new MemoryStream())
            {
                //Se asigna el body del response en una variable y se le da el valor de memorystream
                var originalBody = context.Response.Body;
                context.Response.Body = ms;

                // Permite a la tubería de procesos continuar
                await siguiente(context);

                // Guardamos lo que le respondemos al cliente en el string
                ms.Seek(0, SeekOrigin.Begin);
                string response = new StreamReader(ms).ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);

                // Leemos el stream y lo colocamos como estaba
                await ms.CopyToAsync(originalBody);
                context.Response.Body = originalBody;

                logger.LogInformation(response);
            }
        }
    }
}
