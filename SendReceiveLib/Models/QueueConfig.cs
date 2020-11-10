using SendReceiveLib.Interfaces;
using SendReceiveLib.Utils;
using System.Collections.Generic;

namespace SendReceiveLib.Models
{
    public class QueueConfig : IQueueConfig
    {
        public string QueueName { get; set; } = Consts.RabbitMQQueueName;
        public bool IsDurable { get; set; } = false;
        public bool IsExclusive { get; set; } = false;
        public bool IsAutoDelete { get; set; } = false;
        public IDictionary<string, object> Args { get; set; }
    }
}