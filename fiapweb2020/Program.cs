using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

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
            services.AddMvc();

        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseStaticFiles();

            app.UseMvc(
                routes =>
                {
                    routes.MapRoute(
                    name: "rota1",
                    template: "modulo/{action=Index}/{id?}",
                    defaults:new { controller="Teste"});

                    routes.MapRoute(
                    name: "rota2",
                    template: "{controller=Home}/{action=Index}/{id?}");
                });

            //plugando no pipeline
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Boa noite");
            //});

        }
    }
}