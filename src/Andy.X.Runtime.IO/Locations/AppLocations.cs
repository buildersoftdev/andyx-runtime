namespace Andy.X.Runtime.IO.Locations
{
    public static class AppLocations
    {

        #region Directories
        public static string GetRootDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public static string DataDirectory()
        {
            return Path.Combine(GetRootDirectory(), "data");
        }

        public static string PackagesDirectory()
        {
            return Path.Combine(DataDirectory(), "packages");
        }

        public static string SettingsDirectory()
        {
            return Path.Combine(DataDirectory(), "settings");
        }
        #endregion


        #region settings config files
        public static string GetAndyXConfigurationFile()
        {
            return Path.Combine(SettingsDirectory(), "andyx_config.json");
        }

        public static string GetKafkaConfigurationFile()
        {
            return Path.Combine(SettingsDirectory(), "kafka_config.json");
        }

        public static string GetPulsarConfigurationFile()
        {
            return Path.Combine(SettingsDirectory(), "pulsar_config.json");
        }

        public static string GetArtifactsConfigurationFile()
        {
            return Path.Combine(SettingsDirectory(), "artifacts_config.json");
        }

        public static string GetClusterConfigurationFile()
        {
            return Path.Combine(SettingsDirectory(), "cluster.json");
        }

        public static string GetPackagesConfigurationFile()
        {
            return Path.Combine(SettingsDirectory(), "packages_config.json");
        }
        #endregion

    }
}
