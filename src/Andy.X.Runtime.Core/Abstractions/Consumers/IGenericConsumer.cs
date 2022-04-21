namespace Andy.X.Runtime.Core.Abstractions.Consumers
{
    public interface IGenericConsumer
    {
        public delegate bool TriggerActionHandler(object messageContent);
        public event TriggerActionHandler TriggerAction;

        public bool StartConsuming();
        public bool StopConsuming();
        public bool Unsubscribe();
    }
}
