
namespace Andy.X.Runtime.Core.Abstractions.Hub
{
    public interface IHubService
    {
        bool DownloadArtifcat(Model.Packages.Package package);
        bool InstallArtifact(Model.Packages.Package package, string artifactBinaryLocation);
    }
}
