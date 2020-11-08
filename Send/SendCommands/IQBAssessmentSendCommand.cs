using SendReceiveLib.Interfaces;
using SendReceiveLib.Models;
using System;
using System.Collections.Generic;

namespace Send.SendCommands
{
    public class IQBAssessmentSendCommand<T> : BasicCommand
    {
        public Func<IQBAssessmentMessage, T> OnComplete { get; set; }

        public T Invoke(IPublisherConfig config)
        {
            return Execute(() =>
            {
                var message = new IQBAssessmentMessage();

                Console.WriteLine(config.PromptMessage);

                message.InputMessage = Console.ReadLine();
                message.MessageToSend = string.Format(config.FormattedMessageToSend, message.InputMessage);

                return OnComplete(message);
            });
        }
    }
}
