using Andy.X.Runtime.App.Extensions.DependencyInjection;
using Andy.X.Runtime.Core.Services.App;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Text.Json.Serialization;

namespace Andy.X.Runtime.App
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

            if (Environment.GetEnvironmentVariable("ANDYX_RUNTIME_EXPOSE_CONFIG_ENDPOINTS").ToLower() == "true")
            {
                services.AddControllers()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            }

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Buildersoft Andy X | Runtime", Version = "v1" });
            });

            services.AddSerilogLoggingConfiguration(Configuration);
            services.AddSingleton<ApplicationService>();
            services.AddSingleton<ConfigurationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                if (Environment.GetEnvironmentVariable("ANDYX_RUNTIME_EXPOSE_CONFIG_ENDPOINTS").ToLower() == "true")
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Andy X | Runtime v1"));
                }
            }

            app.UseApplicationService(serviceProvider);

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                // Mapping Rest endpoints
                if (Environment.GetEnvironmentVariable("ANDYX_RUNTIME_EXPOSE_CONFIG_ENDPOINTS").ToLower() == "true")
                    endpoints.MapControllers();
            });
        }
    }
}
