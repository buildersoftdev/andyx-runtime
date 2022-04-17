using Andy.X.Runtime.Core.Configurations;
using Andy.X.Runtime.IO.Locations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Andy.X.Runtime.Core.Services.App
{
    public class ConfigurationService
    {
        private readonly ILogger<ConfigurationService> _logger;

        private ClusterConfiguration? clusterConfiguration;
        private AndyXConfiguration? andyXConfiguration;
        private List<ArtifactConfiguration>? artifactConfigurations;
        private List<PackageConfiguration>? packageConfigurations;

        public ConfigurationService(ILogger<ConfigurationService> logger)
        {
            _logger = logger;

            BindAllConfigurationFiles();
        }

        private void BindAllConfigurationFiles()
        {
            _logger.LogInformation($"Importing settings");
            clusterConfiguration = ImportClusterConfiguration();
            andyXConfiguration = ImportAndyXConfiguration();
            artifactConfigurations = ImportArtifactConfiguration();
            packageConfigurations = ImportPackageConfiguration();
        }

        private ClusterConfiguration? ImportClusterConfiguration()
        {
            string clusterConfigFile = AppLocations.GetClusterConfigurationFile();
            if (File.Exists(clusterConfigFile))
            {
                var cluster = JsonSerializer.Deserialize<ClusterConfiguration>(File.ReadAllText(clusterConfigFile));
                _logger.LogInformation($"Cluster settings with name '{cluster!.Name}' has been imported");

                return cluster;
            }

            _logger.LogError($"Import failed! 'cluster.json' is missing at location '{clusterConfigFile}'");
            return null;
        }

        private AndyXConfiguration? ImportAndyXConfiguration()
        {
            string andyxConfigFile = AppLocations.GetAndyXConfigurationFile();
            if (File.Exists(andyxConfigFile)) 
            {
                var andyx = JsonSerializer.Deserialize<AndyXConfiguration>(File.ReadAllText(andyxConfigFile));
                _logger.LogInformation($"Andy X settings has been imported");
                return andyx;
            }

            _logger.LogError($"Import failed! 'andyx_config.json' is missing at location '{andyxConfigFile}'");
            return null;
        }

        private List<ArtifactConfiguration>? ImportArtifactConfiguration()
        {
            string artifactConfigFile = AppLocations.GetArtifactsConfigurationFile();
            if (File.Exists(artifactConfigFile))
            {
                var artifact = JsonSerializer.Deserialize<List<ArtifactConfiguration>>(File.ReadAllText(artifactConfigFile));
                _logger.LogInformation($"Artifacts have been imported, found {artifact!.Count} artifacts");
                return artifact;
            }

            _logger.LogError($"Import failed! 'artifacts_config.json' is missing at location '{artifactConfigFile}'");
            return null;
        }

        private List<PackageConfiguration>? ImportPackageConfiguration()
        {
            string packageConfigFile = AppLocations.GetPackagesConfigurationFile();
            if (File.Exists(packageConfigFile))
            {
                var package = JsonSerializer.Deserialize<List<PackageConfiguration>>(File.ReadAllText(packageConfigFile));
                _logger.LogInformation($"Packages have been imported, found {package!.Count} artifacts");

                return package;
            }

            _logger.LogError($"Import failed! 'packages_config.json' is missing at location '{packageConfigFile}'");
            return null;
        }
    }
}
