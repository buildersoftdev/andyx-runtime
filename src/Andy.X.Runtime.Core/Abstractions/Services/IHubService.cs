
using Andy.X.Runtime.Model.Packages;

namespace Andy.X.Runtime.Core.Abstractions.Services
{
    public interface IHubService
    {
        bool DownloadArtifcat(Package package);
        bool InstallArtifact(Package package, string artifactBinaryLocation);
    }
}
