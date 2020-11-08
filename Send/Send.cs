using System;
using RabbitMQ.Client;
using SendReceiveLib.Interfaces;
using System.Configuration;
using Send.SendCommands;
using SendReceiveLib.Models;
using Newtonsoft.Json;

namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new SendContainer();
            var queueConfig = container.QueueConfig;
            var publisherConfig = container.PublisherConfig;
            var messageChannel = container.MessageChannel;

            Init(queueConfig, publisherConfig, messageChannel);

            using (var connection = messageChannel.GetConnection<IConnection>())
            {
                using (var channel = messageChannel.GetChannel<IConnection, IModel>(connection, queueConfig))
                {
                    var message = new IQBAssessmentSendCommand<IQBAssessmentMessage>()
                    {
                        OnComplete = message => message
                    }.Invoke(publisherConfig);

                    var isPublished = messageChannel.PublishMessage(publisherConfig, channel, JsonConvert.SerializeObject(message));

                    if (isPublished)
                        Console.WriteLine(" [x] Sent: {0}", message.MessageToSend);
                }
            }

            Console.WriteLine(" [x] Press Enter to Exit.");
            Console.ReadLine();
        }

        private static void Init(IQueueConfig queueConfig, IPublisherConfig publisherConfig, IMessageChannel messageChannel)
        {
            messageChannel.Init(ConfigurationManager.AppSettings.Get("HostName"), queueConfig);
        }
    }
}