using Andy.X.Runtime.Core.Abstractions.Hub;
using Andy.X.Runtime.Core.Abstractions.Package;
using Andy.X.Runtime.Core.Abstractions.Runtime;
using Andy.X.Runtime.Core.Services.App;
using Microsoft.Extensions.Logging;

namespace Andy.X.Runtime.Core.Services.Package
{
    public class PackageLoaderService : IPackageLoaderService
    {
        private readonly ILogger<PackageLoaderService> _logger;
        private readonly ConfigurationService _configurationService;
        private readonly IRuntimeService _runtimeService;

        public PackageLoaderService(ILogger<PackageLoaderService> logger,
            ConfigurationService configurationService,
            IRuntimeService runtimeService)
        {
            _logger = logger;

            _configurationService = configurationService;
            _runtimeService = runtimeService;


            if (configurationService.IsConfigured() != true)
            {
                // do not continue later_on
                return;
            }

            LoadPackagesFromConfiguration();
        }

        private void LoadPackagesFromConfiguration()
        {
            foreach (var package in _configurationService.PackageInstalled)
            {
                if (package.Package.Type == Model.Packages.PackageType.Extension)
                {
                    _runtimeService.AddExtensionProcess(package);
                }
                else
                {
                    // Ignore other types for now.
                }
            }
        }
    }
}
