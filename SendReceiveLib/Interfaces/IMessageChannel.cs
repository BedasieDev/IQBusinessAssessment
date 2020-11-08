using System;

namespace SendReceiveLib.Interfaces
{
    public interface IMessageChannel
    {
        void Init(string hostName, IQueueConfig config);
        T GetConnection<T>();
        O GetChannel<I, O>(I connection, IQueueConfig config);
        bool PublishMessage<T>(IPublisherConfig client, T channel, string message);
        void InitConsumer<IModel, T>(IConsumerConfig consumerConfig, IModel channel, EventHandler<T> eventHandler);
        void ConsumeMessage<IModel>(IConsumerConfig config, IModel channel);
    }
}
