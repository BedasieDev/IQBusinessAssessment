using RabbitMQ.Client;
using SendReceiveLib.Interfaces;
using SendReceiveLib.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendReceiveLib.Models
{
    public class RabbitMQConsumerConfig : IRabbitMQConsumerConfig
    {
        public string Queue { get; set; } = Consts.RabbitMQQueueName;
        public bool AutoAck { get; set; } = true;
        public IBasicConsumer Consumer { get; set; }
    }
}
