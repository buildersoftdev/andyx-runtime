using Andy.X.Runtime.Core.Abstractions.Extension;
using Andy.X.Runtime.Core.Services.App;
using Andy.X.Runtime.Model.Extensions;
using Andy.X.Runtime.Model.Packages;
using Microsoft.Extensions.Logging;

namespace Andy.X.Runtime.Core.Services.Extension
{
    public class ExtensionRuntime : IExtensionRuntime
    {
        private readonly ILogger<ExtensionRuntime> _logger;
        private readonly ConfigurationService _configurationService;
        private readonly Model.Packages.Package _package;
        private readonly PackageSource _packageSource;
        private readonly PackageSettings _packageSettings;

        private bool isRunning;
        public ExtensionStatus Status { get; set; }

        public ExtensionRuntime(ILogger<ExtensionRuntime> logger, ConfigurationService configurationService, Model.Packages.Package package, PackageSource packageSource, PackageSettings packageSettings)
        {
            _logger = logger;
            _configurationService = configurationService;
            _package = package;
            _packageSource = packageSource;
            _packageSettings = packageSettings;

            isRunning = false;
        }


        public bool InvokeExtension(Model.Packages.Package package, object messageContent)
        {
            throw new NotImplementedException();
        }

        public bool IsRunning()
        {
            return isRunning;
        }

        public Task<bool> StartAsync()
        {
            isRunning = true;
            throw new NotImplementedException();
        }

        public Task<bool> StopAsync()
        {
            isRunning = false;
            throw new NotImplementedException();
        }

        public Task<bool> Unsubscribe()
        {
            isRunning = false;
            throw new NotImplementedException();
        }
    }
}
