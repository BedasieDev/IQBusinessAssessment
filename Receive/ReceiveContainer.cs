﻿using SendReceiveLib.Interfaces;
using SendReceiveLib.Utils;

namespace Receive
{
    public class ReceiveContainer
    {
        private UnityResolver UnityResolver;
        internal IQueueConfig QueueConfig;
        internal IRabbitMQConsumerConfig ConsumerConfig;
        internal IMessageChannel MessageChannel;

        public ReceiveContainer()
        {
            UnityResolver = new UnityResolver();

            QueueConfig = UnityResolver.Resolve<IQueueConfig>();
            ConsumerConfig = UnityResolver.Resolve<IRabbitMQConsumerConfig>();
            MessageChannel = UnityResolver.Resolve<IMessageChannel>();
        }
    }
}
