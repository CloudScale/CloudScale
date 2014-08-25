using System.Threading.Tasks;
using CloudScale.Movies.Messages;
using CloudScale.SignalR.Hubs;
using Microsoft.AspNet.SignalR;
using Nimbus.Handlers;

namespace CloudScale.Movies.DataService.Handlers
{
    public class MovieScoreHandler : IHandleMulticastEvent<NewScoreEvent>
    {
        private readonly IHubContext movieHub;

        public MovieScoreHandler()
        {
            movieHub = GlobalHost.ConnectionManager.GetHubContext<MovieHub>();
        }

        public async Task Handle(NewScoreEvent busEvent)
        {
            await
                movieHub.Clients.All.Notify(busEvent.UserId + " just scored " + busEvent.MovieId + " " + busEvent.Score);
        }
    }
}