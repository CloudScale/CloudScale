using System.Threading.Tasks;
using CloudScale.Movies.Messages;
using Nimbus.Handlers;

namespace CloudScale.Movies.LookupService.Handlers
{
	public class PingHandler : IHandleMulticastRequest<PingRequest, PingResponse> 
	{
		public async Task<PingResponse> Handle(PingRequest request)
		{
			return await Task.Run(() => new PingResponse
			{
				Details = GetType() + " : " + GetType().Assembly.GetName().Version,
				HealthCheck = Metrics.HealthChecks.GetStatus()
			});
		}
	}
}