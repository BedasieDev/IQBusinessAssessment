using System.Collections.Generic;

namespace SendReceiveLib.Interfaces
{
    public interface IQueueConfig
    {
        public string QueueName { get; set; }
        public bool IsDurable { get; set; }
        public bool IsExclusive { get; set; }
        public bool IsAutoDelete { get; set; }
        public IDictionary<string, object> Args { get; set; }
    }
}
