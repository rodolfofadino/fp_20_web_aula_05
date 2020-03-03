using Microsoft.AspNetCore.Builder;

namespace fiapweb2020.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMeuMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MeuMiddleware>();
        }
    }



}
