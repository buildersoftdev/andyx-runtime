using Andy.X.Runtime.Core.Abstractions.Extension;
using Andy.X.Runtime.Core.Abstractions.Runtime;
using Andy.X.Runtime.Core.Configurations;
using Andy.X.Runtime.Core.Services.App;
using Andy.X.Runtime.Core.Services.Extension;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Andy.X.Runtime.Core.Services.Runtime
{
    public class RuntimeService : IRuntimeService
    {
        // key: packageId:packageVersion
        private readonly ConcurrentDictionary<string, IExtensionRuntime> _extensionRuntimes;

        private readonly ILogger<RuntimeService> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ConfigurationService _configurationService;

        public RuntimeService(ILoggerFactory loggerFactory, ConfigurationService configurationService)
        {
            _logger = loggerFactory.CreateLogger<RuntimeService>();
            _extensionRuntimes = new ConcurrentDictionary<string, IExtensionRuntime>();
            _loggerFactory = loggerFactory;
            _configurationService = configurationService;
        }

        public bool AddExtensionProcess(PackageConfiguration packageConfiguration)
        {
            string packageId = $"{packageConfiguration.Package.Id}:{packageConfiguration.Package.Version}";
            if (_extensionRuntimes.ContainsKey(packageId))
            {
                return true;
            }

           var isAdded= _extensionRuntimes.TryAdd(packageId,
                            new ExtensionRuntime(
                               _loggerFactory.CreateLogger<ExtensionRuntime>(),
                               _configurationService,

                               packageConfiguration.Package,
                               packageConfiguration.Source,
                               packageConfiguration.Settings));

            if (isAdded)
                _logger.LogInformation($"Extension '{packageId}' is registered into runtime, integrated with '{packageConfiguration.Source.IntegrationSource}'");

            return false;
        }


        // expose extension runtime status to rest api-s
        public IExtensionRuntime GetExtensionRuntime(string packageId)
        {
            if (_extensionRuntimes.ContainsKey(packageId))
                return _extensionRuntimes[packageId];

            return null!;
        }
        public List<IExtensionRuntime> GetExtensionRuntimes()
        {
            return _extensionRuntimes.Values.ToList();
        }
    }
}
