using Metrics;
using Nimbus.MessageContracts;

namespace CloudScale.Movies.Messages
{
    public class PingResponse : IBusMulticastResponse
    {
        public string Details { get; set; }
	    public HealthStatus HealthCheck { get; set; }
    }
}