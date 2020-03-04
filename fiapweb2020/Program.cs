using fiapweb2020.Contexts;
using fiapweb2020.Middlewares;
using fiapweb2020.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddDbContext<ClienteContext>(o=>o.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.AspNetCore.Fiap;Trusted_Connection=True;ConnectRetryCount=0"));
            
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
}
