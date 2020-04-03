using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fiapweb2020.core.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace fiapweb2020.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(
                options =>
                {
                    options.RespectBrowserAcceptHeader = true;
                    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());

                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(x =>
            {
                x.AddPolicy("default", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });

            });

            services.Configure<GzipCompressionProviderOptions>(
                o => o.Level = System.IO.Compression.CompressionLevel.Fastest);
            services.AddResponseCompression(
                o => o.Providers.Add<GzipCompressionProvider>());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Minha API Na Fiap", Version = "v1" });
            });

            services.AddDbContext<ClienteContext>(o => o.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.AspNetCore.Fiap;Trusted_Connection=True;ConnectRetryCount=0"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API");
                });
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseResponseCompression();
            app.UseCors("default");

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
