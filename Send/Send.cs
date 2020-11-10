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
            var display = container.Display;

            Init(queueConfig, publisherConfig, messageChannel);

            using (var connection = messageChannel.GetConnection<IConnection>())
            {
                using (var channel = messageChannel.GetChannel<IConnection, IModel>(connection, queueConfig))
                {
                    var message = new IQBAssessmentSendCommand<IQBAssessmentMessage>()
                    {
                        OnComplete = message => message
                    }.Invoke(publisherConfig, display);

                    var isPublished = messageChannel.PublishMessage(publisherConfig, channel, JsonConvert.SerializeObject(message));

                    if (isPublished)
                        display.DisplayMessage($" [x] Sent: {message.MessageToSend}");
                }
            }

            display.DisplayMessage(" [x] Press Enter to Exit.");
            display.PromptMessage(string.Empty);
        }

        private static void Init(IQueueConfig queueConfig, IPublisherConfig publisherConfig, IMessageChannel messageChannel)
        {
            messageChannel.Init(ConfigurationManager.AppSettings.Get("HostName"), queueConfig);
        }
    }
}