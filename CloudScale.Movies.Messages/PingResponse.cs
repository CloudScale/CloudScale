using Nimbus.MessageContracts;

namespace CloudScale.Movies.Messages
{
    public class PingResponse : IBusResponse
    {
        public string Details { get; set; }
    }
}