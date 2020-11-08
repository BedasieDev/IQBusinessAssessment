using SendReceiveLib.Interfaces;
using SendReceiveLib.Utils;

namespace Send
{
    internal class SendContainer
    {
        private UnityResolver UnityResolver;
        internal IQueueConfig QueueConfig;
        internal IRabbitMQPublisherConfig PublisherConfig;
        internal IMessageChannel MessageChannel;

        public SendContainer()
        {
            UnityResolver = new UnityResolver();

            QueueConfig = UnityResolver.Resolve<IQueueConfig>();
            PublisherConfig = UnityResolver.Resolve<IRabbitMQPublisherConfig>();
            MessageChannel = UnityResolver.Resolve<IMessageChannel>();
        }
    }
}