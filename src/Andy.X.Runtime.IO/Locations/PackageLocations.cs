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
                return Directory.GetFiles(GetPackageSourceDirectory(packageId, version));
            }
            catch (Exception)
            {
                return new List<string>()
                    .ToArray();
            }
        }
    }
}
