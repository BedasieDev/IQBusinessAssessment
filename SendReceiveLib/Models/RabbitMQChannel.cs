using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SendReceiveLib.Interfaces;
using System;
using System.Text;

namespace SendReceiveLib.Models
{
    public class RabbitMQChannel : IMessageChannel
    {
        private ConnectionFactory connectionFactory;

        public void Init(string hostName, IQueueConfig config)
        {
            connectionFactory = new ConnectionFactory() { HostName = hostName };
        }

        public IConnection GetConnection<IConnection>()
        {
            return (IConnection)connectionFactory.CreateConnection();
        }

        public IModel GetChannel<IConnection, IModel>(IConnection connection, IQueueConfig config)
        {
            var channel = (connection as RabbitMQ.Client.IConnection).CreateModel();

            channel.QueueDeclare(queue: config.QueueName,
                                 durable: config.IsDurable,
                                 exclusive: config.IsExclusive,
                                 autoDelete: config.IsAutoDelete,
                                 arguments: config.Args);

            return (IModel)channel;
        }

        public bool PublishMessage<IModel>(IPublisherConfig rabbitMQPublisherConfig, IModel channel, string message)
        {
            try
            {
                var body = Encoding.Default.GetBytes(message);

                (channel as RabbitMQ.Client.IModel)
                    .BasicPublish(exchange: rabbitMQPublisherConfig.ExchangeName,
                                  routingKey: rabbitMQPublisherConfig.RoutingKey,
                                  basicProperties: (rabbitMQPublisherConfig as IRabbitMQPublisherConfig).BasicProperties,
                                  body: body);

                return true;
            }
            catch (Exception) { }

            return false;
        }

        public void InitConsumer<IModel, T>(IConsumerConfig rabbitMQConsumerConfig, IModel channel, EventHandler<T> eventHandler)
        {
            var config = (IRabbitMQConsumerConfig)rabbitMQConsumerConfig;

            config.Consumer = new EventingBasicConsumer((RabbitMQ.Client.IModel)channel);
            (config.Consumer as EventingBasicConsumer).Received += (eventHandler as EventHandler<BasicDeliverEventArgs>);
        }

        public void ConsumeMessage<IModel>(IConsumerConfig rabbitMQConsumerConfig, IModel channel)
        {
            (channel as RabbitMQ.Client.IModel)
                .BasicConsume(queue: rabbitMQConsumerConfig.Queue,
                              autoAck: rabbitMQConsumerConfig.AutoAck,
                              consumer: (rabbitMQConsumerConfig as IRabbitMQConsumerConfig).Consumer);
        }
    }
}
