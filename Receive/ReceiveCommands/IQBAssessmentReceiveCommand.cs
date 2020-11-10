using Newtonsoft.Json;
using SendReceiveLib.Interfaces;
using SendReceiveLib.Models;
using System;
using System.Text;

namespace Receive.ReceiveCommands
{
    public class IQBAssessmentReceiveCommand<T> : BasicCommand
    {
        public Func<bool, T> OnComplete { get; set; }

        public T Invoke(byte[] message, IDisplay display)
        {
            return Execute(() =>
            {
                var iqbMessage = JsonConvert.DeserializeObject<IQBAssessmentMessage>(Encoding.UTF8.GetString(message));

                if (string.IsNullOrEmpty(iqbMessage.InputMessage))
                {
                    display.DisplayMessage(" [x] No Valid Message Received");
                    return OnComplete(false);
                }

                display.DisplayMessage($" [x] Received: Salut {iqbMessage.InputMessage}, je suis ton père!");
                return OnComplete(true);
            });
        }
    }
}