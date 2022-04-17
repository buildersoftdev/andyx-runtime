namespace Andy.X.Runtime.Model.Packages
{
    public class Package
    {
        public string? Name { get; set; }
        public string? Id { get; set; }
        public string? Version { get; set; }
        public PackageType Type { get; set; }

        public Package()
        {
            Type = PackageType.Extension;
        }
    }
}
