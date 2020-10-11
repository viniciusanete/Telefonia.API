using System;
using Swashbuckle.AspNetCore.Swagger;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Telefonia.Host.Infrastructure.ExtensionMethods;
using Microsoft.OpenApi.Models;

namespace Telefonia.Host
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; set; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Telefonia.Context.Common.IDbSettings, Telefonia.Infrastructure.Data.Config.DbSettings>((o) => new Telefonia.Infrastructure.Data.Config.DbSettings
            {
                ConnectionString = Configuration.GetValue<string>("ConnectionString")
            });

            services.AddDomain();
            services.AddControllers();

            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddCheck("DataBase", new Infrastructure.HealthCheck.SqliteConnectionHealthCheck(Configuration.GetValue<string>("ConnectionString")),
                                                   HealthStatus.Unhealthy,
                                                   new string[] { "DataBase" });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Cadastro de Plano de Telefonia",
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Name = "Vinicius Meireles Anete"
                        }
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new Middleware.JsonExceptionMiddleware(env).Invoke
            });

            app.UseHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cadastro de Plano de Telefonia V1");
            });


            Telefonia.Infrastructure.Data.Repository.RegisterMappings.Register();

            app.UseCors("CorsPolicy");
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
