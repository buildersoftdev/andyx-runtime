using Andy.X.Runtime.Core.Abstractions.Consumers;
using Andy.X.Runtime.Core.Abstractions.Extension;
using Andy.X.Runtime.Core.Consumers.Extensions;
using Andy.X.Runtime.Core.Contexts;
using Andy.X.Runtime.Core.Services.App;
using Andy.X.Runtime.IO.Locations;
using Andy.X.Runtime.Model.Extensions;
using Andy.X.Runtime.Model.Packages;
using Cortex.Core.Abstractions.Extensions;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Runtime.Loader;

namespace Andy.X.Runtime.Core.Services.Extension
{
    public class ExtensionRuntime : IExtensionRuntime
    {
        private readonly ILogger<ExtensionRuntime> _logger;
        private readonly ConfigurationService _configurationService;
        private readonly Model.Packages.Package _package;
        private readonly PackageSource _packageSource;
        private readonly PackageSettings _packageSettings;

        private IGenericConsumer? consumer;

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
            Status = ExtensionStatus.Stopped;

            if (packageSource.IntegrationSource == PackageIntegrationSource.AndyX)
            {
                consumer = new AndyXGenericConsumer(configurationService.AndyXConfiguration, package, packageSource);
                consumer.TriggerAction += Consumer_TriggerAction;
            }

            isRunning = consumer!
                .StartConsuming();

            if (isRunning == true)
                Status = ExtensionStatus.Running;
        }

        private bool Consumer_TriggerAction(object messageContent)
        {
            return InvokeExtension(_package, messageContent);
        }

        public bool InvokeExtension(Model.Packages.Package package, object messageContent)
        {
            bool result = false;
            if (_packageSource.IntegrationSource == PackageIntegrationSource.AndyX)
            {
                result = LoadAndExecuteExtensionAssembly(package, messageContent);
            }

            if (_packageSettings.SkipMessageIfFails == true)
                result = true;

            return result;
        }

        private bool LoadAndExecuteExtensionAssembly(Model.Packages.Package package, object messageContent)
        {
            bool result = false;
            var assembly = new ExtensionAssemblyLoadContext(PackageLocations.GetPackageSourceFile(package.Id!, package.Version!, PackageIntegrationSource.AndyX));
            Assembly source = assembly.LoadFromAssemblyPath(PackageLocations.GetPackageSourceFile(package.Id!, package.Version!, PackageIntegrationSource.AndyX));

            var startupClass = source
                .GetTypes()
                .Where(t => t.GetInterfaces().Where(i => i.FullName == "Cortex.Core.Abstractions.Extensions.IExtensionStartup")
                .ToList().Count != 0 && t.IsClass && t.FullName!.Contains("Startup")).FirstOrDefault();
            try
            {
                if (startupClass != null)
                {
                    IExtensionStartup? startupInstance = source.CreateInstance(startupClass!.FullName!) as IExtensionStartup;
                    result = startupInstance!.Handle(messageContent);
                }
            }
            catch (Exception)
            {
                result = false;
            }
         

            assembly.Unload();

            return result;
        }

        public bool IsRunning()
        {
            return isRunning;
        }

        public bool Start()
        {
            isRunning = true;
            return consumer!.StartConsuming();
        }

        public bool Stop()
        {
            isRunning = false;
            return consumer!.StopConsuming();
        }

        public bool Unsubscribe()
        {
            isRunning = false;
            return consumer!.Unsubscribe();
        }
    }
}
