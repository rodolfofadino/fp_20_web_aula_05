using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Text;
using System.Threading.Tasks;

namespace fiapweb2020.Middlewares
{
    public class MeuMiddleware
    {
        private RequestDelegate _next;

        public MeuMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Request.EnableRewind();
            var request = await FormatRequest(httpContext.Request);

            var log = new LoggerConfiguration()
                .WriteTo.Logentries("fc2c1b88-b5d9-4e41-8fbd-46755ed66f59")
                .CreateLogger();

            log.Information($"request {request}");

            httpContext.Request.Body.Position = 0;
            //
            await _next(httpContext);
            //

        }
        private static async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;
            
            request.EnableRewind();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;

            var messageObjToLog = new { scheme = request.Scheme, host = request.Host, path = request.Path, queryString = request.Query, requestBody = bodyAsText };

            return JsonConvert.SerializeObject(messageObjToLog);
        }

    }



}
