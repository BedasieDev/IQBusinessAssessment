using RabbitMQ.Client;
using SendReceiveLib.Interfaces;
using SendReceiveLib.Utils;

namespace SendReceiveLib.Models
{
    public class RabbitMQPublisherConfig : IRabbitMQPublisherConfig
    {
        public string PromptMessage { get; set; } = "Name:";
        public string FormattedMessageToSend { get; set; } = "Hello my name is, {0}";
        public string ExchangeName { get; set; } = string.Empty;
        public string RoutingKey { get; set; } = Consts.RabbitMQQueueName;
        public IBasicProperties BasicProperties { get; set; }
    }
}