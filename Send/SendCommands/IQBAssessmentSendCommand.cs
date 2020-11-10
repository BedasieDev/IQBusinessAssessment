using SendReceiveLib.Interfaces;
using SendReceiveLib.Models;
using System;

namespace Send.SendCommands
{
    public class IQBAssessmentSendCommand<T> : BasicCommand
    {
        public Func<IQBAssessmentMessage, T> OnComplete { get; set; }

        public T Invoke(IPublisherConfig config, IDisplay display)
        {
            return Execute(() =>
            {
                var message = new IQBAssessmentMessage();

                message.InputMessage = display.PromptMessage(config.PromptMessage);
                message.MessageToSend = string.Format(config.FormattedMessageToSend, message.InputMessage);

                return OnComplete(message);
            });
        }
    }
}