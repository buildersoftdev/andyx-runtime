namespace Andy.X.Runtime.Core.Configurations
{
    public class ClusterConfiguration
    {
        public string? Name { get; set; }
        public List<RuntimeConfiguration> Runtimes { get; set; }
        public ClusterConfiguration()
        {
            Runtimes = new List<RuntimeConfiguration>();
        }
    }
    public class RuntimeConfiguration
    {
        public string? Hostname { get; set; }
        public string[]? Ports { get; set; }
    }
}
