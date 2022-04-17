using Andy.X.Runtime.Model.Packages;

namespace Andy.X.Runtime.Core.Configurations
{
    public class PackageConfiguration
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public Package Package { get; set; }
        public PackageSettings Settings { get; set; }
        public PackageSource Source { get; set; }

        public PackageConfiguration()
        {
            Package = new Package();
            Settings = new PackageSettings();
            Source = new PackageSource();
        }
    }
}
