using Andy.X.Runtime.Core.Abstractions.Services;
using Andy.X.Runtime.Core.Configurations;
using Andy.X.Runtime.Model.Packages;
using Microsoft.Extensions.Logging;

namespace Andy.X.Runtime.Core.Services.Hub
{
    public class HubService : IHubService
    {
        private readonly ILogger<HubService> _logger;
        private readonly ArtifactConfiguration _artifactConfiguration;

        public HubService(ILogger<HubService> logger, ArtifactConfiguration artifactConfiguration)
        {
            _logger = logger;
            _artifactConfiguration = artifactConfiguration;
        }

        public bool DownloadArtifcat(Package package)
        {
            _logger.LogError("Implement - DownloadArtifcat");
            return false;
        }

        public bool InstallArtifact(Package package, string artifactBinaryLocation)
        {
            _logger.LogError("Implement - InstallArtifact");
            return false;
        }
    }
}
