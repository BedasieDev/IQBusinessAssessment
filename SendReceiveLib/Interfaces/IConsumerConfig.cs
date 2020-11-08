namespace SendReceiveLib.Interfaces
{
    public interface IConsumerConfig
    {
        string Queue { get; set; }
        bool AutoAck { get; set; }
    }
}