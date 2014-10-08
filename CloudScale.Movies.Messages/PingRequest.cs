using Nimbus.MessageContracts;

namespace CloudScale.Movies.Messages
{
    public class PingRequest : IBusMulticastRequest<PingRequest, PingResponse>
    {
    }
}