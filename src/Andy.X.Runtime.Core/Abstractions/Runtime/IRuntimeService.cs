using Andy.X.Runtime.Core.Abstractions.Extension;
using Andy.X.Runtime.Core.Configurations;

namespace Andy.X.Runtime.Core.Abstractions.Runtime
{
    public interface IRuntimeService
    {
        bool AddExtensionProcess(PackageConfiguration packageConfiguration);

        IExtensionRuntime GetExtensionRuntime(string packageId);
        List<IExtensionRuntime> GetExtensionRuntimes();
    }
}
