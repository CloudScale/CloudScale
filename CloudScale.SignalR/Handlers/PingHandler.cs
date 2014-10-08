using System.Threading.Tasks;
using CloudScale.Movies.Messages;
using CloudScale.SignalR.Hubs;
using Microsoft.AspNet.SignalR;
using Nimbus.Handlers;

namespace CloudScale.SignalR.Handlers
{
	public class PingHandler : IHandleMulticastRequest<PingRequest, PingResponse>
	{
        private readonly IHubContext movieHub;

        public PingHandler()
        {
            movieHub = GlobalHost.ConnectionManager.GetHubContext<MovieHub>();
        }

        public async Task<PingResponse> Handle(PingRequest request)
        {
            movieHub.Clients.All.Ping();

            return await Task.Run(() => new PingResponse
            {
	            Details = GetType() + " : " + GetType().Assembly.GetName().Version,
	            HealthCheck = Metrics.HealthChecks.GetStatus()
            });
        }
    }
}