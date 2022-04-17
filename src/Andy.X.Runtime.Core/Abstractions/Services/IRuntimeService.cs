using Andy.X.Runtime.Model.Packages;

namespace Andy.X.Runtime.Core.Abstractions.Services
{
    public interface IRuntimeService
    {
        bool InvokeExtension(Package package, object messageContent);
        bool InvokePlugin(Package package);

        bool RestartPlugin(Package package);
    }
}
