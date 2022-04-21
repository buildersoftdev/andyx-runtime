using Andy.X.Runtime.Core.Abstractions.Package;
using Andy.X.Runtime.Core.Abstractions.Runtime;
using Andy.X.Runtime.Core.Services.App;
using Andy.X.Runtime.Core.Services.Package;
using Andy.X.Runtime.Core.Services.Runtime;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Andy.X.Runtime.App.Extensions.DependencyInjection
{
    public static class ApplicationDependencyInjectionExtensions
    {

        public static void AddApplicationServices(this IServiceCollection services)
        {

            services.AddSingleton<ApplicationService>();
            services.AddSingleton<ConfigurationService>();

            services.AddSingleton<IRuntimeService, RuntimeService>();

            services.AddSingleton<IPackageLoaderService, PackageLoaderService>();
        }


        public static void UseApplicationService(this IApplicationBuilder builder, IServiceProvider serviceProvider)
        {
            var appService = serviceProvider.GetRequiredService<ApplicationService>();
            var configService = serviceProvider.GetRequiredService<ConfigurationService>();

            var packageLoaderService = serviceProvider.GetRequiredService<IPackageLoaderService>();
        }
    }
}
