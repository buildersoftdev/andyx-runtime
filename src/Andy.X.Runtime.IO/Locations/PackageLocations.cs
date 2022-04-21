using Andy.X.Runtime.Model.Packages;

namespace Andy.X.Runtime.IO.Locations
{
    public static class PackageLocations
    {
        public static string GetPackageSourceDirectory(string packageId, string version)
        {
            return Path.Combine(AppLocations.PackagesDirectory(), packageId, version);
        }

        public static string[] GetPackageSourceFiles(string packageId, string version)
        {
            try
            {
                return Directory.GetFiles(GetPackageSourceDirectory(packageId, version), "*.dll");
            }
            catch (Exception)
            {
                return new List<string>()
                    .ToArray();
            }
        }


        public static string GetPackageSourceFile(string packageId, string version, PackageIntegrationSource packageIntegrationSource)
        {
            string[] sourceFiles = GetPackageSourceFiles(packageId, version);

            if (packageIntegrationSource == PackageIntegrationSource.AndyX)
                return sourceFiles!.Where(f => f.EndsWith(".AndyX.dll")).FirstOrDefault()!;

            if (packageIntegrationSource == PackageIntegrationSource.Kafka)
                return sourceFiles!.Where(f => f.EndsWith(".Kafka.dll")).FirstOrDefault()!;

            if (packageIntegrationSource == PackageIntegrationSource.Pulsar)
                return sourceFiles!.Where(f => f.EndsWith(".Pulsar.dll")).FirstOrDefault()!;

            return null!;
        }
    }
}
