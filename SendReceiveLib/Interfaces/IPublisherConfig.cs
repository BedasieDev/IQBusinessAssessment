namespace SendReceiveLib.Interfaces
{
    public interface IPublisherConfig
    {
        string PromptMessage { get; set; }
        string FormattedMessageToSend { get; set; }
        string ExchangeName { get; set; }
        string RoutingKey { get; set; }
    }
}
