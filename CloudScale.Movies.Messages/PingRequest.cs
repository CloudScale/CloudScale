using Nimbus.MessageContracts;

namespace CloudScale.Movies.Messages
{
    public class PingRequest : IBusRequest<PingRequest, PingResponse>
    {
    }
}