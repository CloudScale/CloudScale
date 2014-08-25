using Nimbus.MessageContracts;

namespace CloudScale.Movies.Messages
{
    public class IsRegisteredResponse : IBusResponse
    {
        public bool Registered { get; set; }
    }
}