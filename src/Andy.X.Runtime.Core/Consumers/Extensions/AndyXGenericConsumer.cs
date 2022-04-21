using Andy.X.Client;
using Andy.X.Runtime.Core.Abstractions.Consumers;
using Andy.X.Runtime.Core.Configurations;
using Andy.X.Runtime.Model.Packages;

namespace Andy.X.Runtime.Core.Consumers.Extensions
{
    public class AndyXGenericConsumer : IGenericConsumer
    {
        private readonly AndyXConfiguration _andyXConfiguration;
        private readonly Package _package;
        private readonly PackageSource _packageSource;

        private XClient xClient;
        private Consumer<object> consumer;

        public event IGenericConsumer.TriggerActionHandler? TriggerAction;

        public AndyXGenericConsumer(AndyXConfiguration andyXConfiguration, Package package, PackageSource packageSource)
        {
            _andyXConfiguration = andyXConfiguration;
            _package = package;
            _packageSource = packageSource;

            xClient = new XClient(andyXConfiguration.NodeServiceUrl)
                .AutoConnect()
                .Tenant(packageSource.Tenant)
                .Product(packageSource.Product);

            if (packageSource.TenantToken != null)
                xClient.TenantToken(packageSource.TenantToken);

            consumer = InitializeConsumer();
        }

        private Consumer<object> InitializeConsumer()
        {
            var consumerBuild = new Consumer<object>(xClient)
                .Name($"{_package.Id!.Replace(".", "-").ToLowerInvariant()}-{_package.Version!.Replace(".", "-")}-runtime")
                .Component(_packageSource.Component)
                .Topic(_packageSource.Topic)
                // we should add InitialPosition at PackageSource
                .InitialPosition(Client.Configurations.InitialPosition.Earliest)
                // it's shared because runtime is a cluster
                .SubscriptionType(Client.Configurations.SubscriptionType.Shared)
                .Build();

            if (_packageSource.ComponentToken != null)
                consumerBuild.ComponentToken(_packageSource.ComponentToken);

            consumerBuild.MessageReceived += ConsumerBuild_MessageReceived;

            return consumerBuild;
        }

        private bool ConsumerBuild_MessageReceived(object sender, Client.Events.Consumers.MessageReceivedArgs<object> e)
        {
            return TriggerAction!.Invoke(e.GenericPayload);
        }

        public bool StartConsuming()
        {
            consumer.SubscribeAsync().Wait();
            return true;
        }

        public bool StopConsuming()
        {
            consumer.UnsubscribeAsync().Wait();
            return true;
        }

        public bool Unsubscribe()
        {
            return false;
        }
    }
}
