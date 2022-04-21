using Andy.X.Runtime.Model.Extensions;

namespace Andy.X.Runtime.Core.Abstractions.Extension
{
    public interface IExtensionRuntime
    {
        public ExtensionStatus Status { get; set; }

        bool InvokeExtension(Model.Packages.Package package, object messageContent);

        bool Start();
        bool Stop();

        
        bool Unsubscribe();

        bool IsRunning();
    }
}
