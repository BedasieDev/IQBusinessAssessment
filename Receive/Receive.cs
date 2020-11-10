using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Receive.ReceiveCommands;
using SendReceiveLib.Interfaces;
using SendReceiveLib.Models;
using System;
using System.Configuration;
using System.Text;

namespace Receive
{
    class Program
    {
        private static IDisplay display;

        static void Main(string[] args)
        {
            var container = new ReceiveContainer();
            var queueConfig = container.QueueConfig;
            var consumerConfig = container.ConsumerConfig;
            var messageChannel = container.MessageChannel;
            display = container.Display;

            Init(queueConfig, messageChannel);

            using (var connection = messageChannel.GetConnection<IConnection>())
            {
                using (var channel = messageChannel.GetChannel<IConnection, IModel>(connection, queueConfig))
                {
                    messageChannel.InitConsumer(consumerConfig, channel, new EventHandler<BasicDeliverEventArgs>(MessageReceived));
                    messageChannel.ConsumeMessage(consumerConfig, channel);
                    display.DisplayMessage(" [x] Press Enter to Exit.");
                    display.PromptMessage(string.Empty);
                }
            }
        }

        public static void MessageReceived(object sender, BasicDeliverEventArgs e)
        {
            var received = new IQBAssessmentReceiveCommand<bool>
            {
                OnComplete = received => received
            }.Invoke(e.Body.ToArray(), display);
        }

        private static void Init(IQueueConfig queueConfig, IMessageChannel messageChannel)
        {
            messageChannel.Init(ConfigurationManager.AppSettings.Get("HostName"), queueConfig);
        }
    }
}