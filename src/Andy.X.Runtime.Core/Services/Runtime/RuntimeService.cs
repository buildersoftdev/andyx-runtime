using Andy.X.Runtime.Core.Abstractions.Services;
using Andy.X.Runtime.Model.Packages;
using Microsoft.Extensions.Logging;

namespace Andy.X.Runtime.Core.Services.Runtime
{
    public class RuntimeService : IRuntimeService
    {
        private readonly ILogger<RuntimeService> _logger;

        public RuntimeService(ILogger<RuntimeService> logger)
        {
            _logger = logger;
        }

        public bool InvokeExtension(Package package, object messageContent)
        {
            _logger.LogError("Not implemented - InvokeExtension");
            return false;
        }

        public bool InvokePlugin(Package package)
        {
            _logger.LogError("Not implemented - InvokePlugin");
            return false;
        }

        public bool RestartPlugin(Package package)
        {
            _logger.LogError("Not implemented - RestartPlugin");
            return false;
        }
    }
}
