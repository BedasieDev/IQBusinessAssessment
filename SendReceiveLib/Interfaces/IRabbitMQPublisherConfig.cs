using RabbitMQ.Client;

namespace SendReceiveLib.Interfaces
{
    public interface IRabbitMQPublisherConfig : IPublisherConfig
    {
        IBasicProperties BasicProperties { get; set; }

    }
}
