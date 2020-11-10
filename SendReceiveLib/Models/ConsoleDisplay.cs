using SendReceiveLib.Interfaces;
using System;

namespace SendReceiveLib.Models
{
    public class ConsoleDisplay : IDisplay
    {
        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public string PromptMessage(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
    }
}
