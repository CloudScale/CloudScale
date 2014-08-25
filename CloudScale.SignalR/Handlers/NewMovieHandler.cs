using System.Threading.Tasks;
using CloudScale.Movies.Messages;
using CloudScale.SignalR.Hubs;
using Microsoft.AspNet.SignalR;
using Nimbus.Handlers;

namespace CloudScale.Movies.DataService.Handlers
{
    public class NewMovieHandler : IHandleMulticastEvent<NewMovieEvent>
    {
        private readonly IHubContext movieHub;

        public NewMovieHandler()
        {
            movieHub = GlobalHost.ConnectionManager.GetHubContext<MovieHub>();
        }

        public async Task Handle(NewMovieEvent busEvent)
        {
            await movieHub.Clients.All.Notify("A new movie has just been added " + busEvent.Movie.Name);
        }
    }
}