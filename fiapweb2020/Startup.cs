using fiapweb2020.core.Contexts;
using fiapweb2020.Middlewares;
using fiapweb2020.core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Net.Http.Headers;

namespace fiapweb2020
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddTransient<INoticiaService, NoticiaService>();
            //services.AddScoped<INoticiaService, NoticiaService>();
            services.AddSingleton<INoticiaService, NoticiaService>();

            //services.AddDataProtection()
            //    .SetApplicationName("admin")
            //    .PersistKeysToFileSystem(new DirectoryInfo(@"c:/teste/"));

            services.AddDbContext<ClienteContext>(o => o.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.AspNetCore.Fiap;Trusted_Connection=True;ConnectRetryCount=0"));

            services.Configure<GzipCompressionProviderOptions>(
                o => o.Level = System.IO.Compression.CompressionLevel.Fastest);

            services.AddResponseCompression(
                o => o.Providers.Add<GzipCompressionProvider>());

            services.AddAuthentication("app")
                .AddCookie("app", o =>
                {
                    o.LoginPath = "/account/index";
                    o.AccessDeniedPath = "/account/denied";
                });

            services.AddMemoryCache();

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
            else
            {
                app.UseExceptionHandler("/home/error");
            }

            //app.UseExceptionHandler(errorApp =>
            //{
            //    errorApp.Run(async context =>
            //    {
            //        context.Response.StatusCode = 500;
            //        context.Response.ContentType = "text/html";

            //        await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
            //        await context.Response.WriteAsync("ERROR!<br><br>\r\n");

            //        var exceptionHandlerPathFeature =
            //            context.Features.Get<IExceptionHandlerPathFeature>();

            //        // Use exceptionHandlerPathFeature to process the exception (for example, 
            //        // logging), but do NOT expose sensitive error information directly to 
            //        // the client.

            //        if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
            //        {
            //            await context.Response.WriteAsync("File error thrown!<br><br>\r\n");
            //        }

            //        await context.Response.WriteAsync("<a href=\"/\">Home</a><br>\r\n");
            //        await context.Response.WriteAsync("</body></html>\r\n");
            //        await context.Response.WriteAsync(new string(' ', 512)); // IE padding
            //    });
            //});

            app.UseResponseCompression();

            app.UseAuthentication();

            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = ctx =>
                {
                    var yearDurationInSeconds = 60 * 60 * 24* 365;
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] = $"public, max-age={yearDurationInSeconds}";
                }
            });



            //app.UseResponseCompression();

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
