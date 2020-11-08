using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Receive.ReceiveCommands;
using SendReceiveLib.Interfaces;
using System;
using System.Configuration;

namespace Receive
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new ReceiveContainer();
            var queueConfig = container.QueueConfig;
            var consumerConfig = container.ConsumerConfig;
            var messageChannel = container.MessageChannel;

            Init(queueConfig, messageChannel);

            using (var connection = messageChannel.GetConnection<IConnection>())
            {
                using (var channel = messageChannel.GetChannel<IConnection, IModel>(connection, queueConfig))
                {
                    messageChannel.InitConsumer(consumerConfig, channel,
                        new EventHandler<BasicDeliverEventArgs>(new IQBAssessmentReceiveCommand().Invoke));
                    messageChannel.ConsumeMessage(consumerConfig, channel);
                    Console.WriteLine(" [x] Press Enter to Exit.");
                    Console.ReadLine();
                }
            }
        }

        private static void Init(IQueueConfig queueConfig, IMessageChannel messageChannel)
        {
            messageChannel.Init(ConfigurationManager.AppSettings.Get("HostName"), queueConfig);
        }
    }
}