using fiapweb2020.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Text;
using System.Threading.Tasks;

namespace fiapweb2020
{
    public class Program
    {
        public static void Main(string[] args)
        {

            BuildWebHost(args).Run();

            //Console.WriteLine("Hello World!");
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        }

    }

    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddTransient<INoticiaService, NoticiaService>();
            //services.AddScoped<INoticiaService, NoticiaService>();
            services.AddSingleton<INoticiaService, NoticiaService>();


            services.AddMvc();

        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            #region middleware exemplos


            // app.Use(async (context, next) =>
            // {
            //     //antes
            //     await next.Invoke();
            //     //depois
            // });


            // app.Map("/admin", mapApp =>
            //{
            //    mapApp.Run(async context =>
            //    {
            //        await context.Response.WriteAsync("Admin");
            //    });
            //});

            // app.MapWhen(
            //     context => context.Request.Query.ContainsKey("queryTeste"),
            //     mapApp =>
            //     {
            //         mapApp.Run(async context =>
            //         {
            //             await context.Response.WriteAsync("Hello Fiap!");
            //         });
            //     }
            // );



            // app.Use(async (context, next) =>
            // {
            //     //antes
            //     await next.Invoke();
            //     //depois
            // });

            // app.Run(async context =>
            // {
            //     await context.Response.WriteAsync("Hello from 2nd delegate.");
            // }
            // );

            #endregion

            //app.UseRequestLocalization(new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = new RequestCulture("pt-br"),
            //    // Formatting numbers, dates, etc.
            //    SupportedCultures = supportedCultures,
            //    // UI strings that we have localized.
            //    SupportedUICultures = supportedCultures
            //});

            //app.UseMiddleware<MeuMiddleware>();

            app.UseMeuMiddleware();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(
                routes =>
                {
                    routes.MapRoute(
                    name: "rota1",
                    template: "modulo/{action=Index}/{id?}",
                    defaults: new { controller = "Teste" });

                    routes.MapRoute(
                    name: "rota2",
                    template: "{controller=Home}/{action=Index}/{id?}");
                });


        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMeuMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MeuMiddleware>();
        }
    }

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
