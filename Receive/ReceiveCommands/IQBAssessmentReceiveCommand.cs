using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using SendReceiveLib.Models;
using System;
using System.Text;

namespace Receive.ReceiveCommands
{
    public class IQBAssessmentReceiveCommand
    {
        public void Invoke(object obj, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();

            var iqbAssessmentMessage = JsonConvert.DeserializeObject<IQBAssessmentMessage>(Encoding.UTF8.GetString(body));

            if (string.IsNullOrEmpty(iqbAssessmentMessage.InputMessage))
                Console.WriteLine(" [x] No Valid Message Received");
            else
                Console.WriteLine($" [x] Received: Salut {iqbAssessmentMessage.InputMessage}, je suis ton père!");
        }
    }
}