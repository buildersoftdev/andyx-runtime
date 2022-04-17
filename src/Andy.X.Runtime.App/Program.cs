using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Andy.X.Runtime.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .CreateLogger();

            // SETTING environment variables for Env, Cert and default asp_net
            if (Environment.GetEnvironmentVariable("ANDYX_RUNTIME_ENVIRONMENT") != null)
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", Environment.GetEnvironmentVariable("ANDYX_ENVIRONMENT"));

            if (Environment.GetEnvironmentVariable("ANDYX_RUNTIME_URLS") != null)
                Environment.SetEnvironmentVariable("ASPNETCORE_URLS", Environment.GetEnvironmentVariable("ANDYX_URLS"));
            else
                Environment.SetEnvironmentVariable("ASPNETCORE_URLS", "https://+:6641;http://+:6640");

            if (Environment.GetEnvironmentVariable("ANDYX_RUNTIME_EXPOSE_CONFIG_ENDPOINTS") == null)
                Environment.SetEnvironmentVariable("ANDYX_RUNTIME_EXPOSE_CONFIG_ENDPOINTS", "true");

            try
            {
                CreateHostBuilder(args).Build().Run();

            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
