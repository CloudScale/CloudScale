using Nimbus.MessageContracts;

namespace CloudScale.Movies.Messages
{
    public class PingResponse : IBusMulticastResponse
    {
        public string Details { get; set; }
    }
}