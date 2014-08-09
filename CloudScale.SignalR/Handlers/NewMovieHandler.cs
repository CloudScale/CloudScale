using CloudScale.Movies.Messages;
using CloudScale.SignalR.Hubs;
using Microsoft.AspNet.SignalR;
using Nimbus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudScale.Movies.DataService.Handlers
{
    public class NewMovieHandler : IHandleMulticastEvent<NewMovieEvent>
    {
        private readonly IHubContext movieHub;

        public NewMovieHandler()
        {
            this.movieHub = GlobalHost.ConnectionManager.GetHubContext<MovieHub>();
        }

        public async Task Handle(NewMovieEvent busEvent)
        {
            await movieHub.Clients.All.Notify("A new movie has just been added " + busEvent.Movie.Name);
        }
    }
}
