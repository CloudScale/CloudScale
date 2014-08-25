using System.Threading.Tasks;
using CloudScale.Movies.Messages;
using CloudScale.SignalR.Hubs;
using Microsoft.AspNet.SignalR;
using Nimbus.Handlers;

namespace CloudScale.Movies.DataService.Handlers
{
    public class LookupMovieHandler : IHandleMulticastEvent<LookupMovieEvent>
    {
        private readonly IHubContext movieHub;

        public LookupMovieHandler()
        {
            movieHub = GlobalHost.ConnectionManager.GetHubContext<MovieHub>();
        }

        public async Task Handle(LookupMovieEvent busEvent)
        {
            await movieHub.Clients.All.Notify("New data is available for " + busEvent.Name);
        }
    }
}