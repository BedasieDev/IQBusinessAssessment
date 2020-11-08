using RabbitMQ.Client;

namespace SendReceiveLib.Interfaces
{
    public interface IRabbitMQConsumerConfig : IConsumerConfig
    {
        IBasicConsumer Consumer { get; set; }

    }
}