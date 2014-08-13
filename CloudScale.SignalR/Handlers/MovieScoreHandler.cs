using CloudScale.Movies.Messages;
using CloudScale.SignalR.Hubs;
using Microsoft.AspNet.SignalR;
using Nimbus;
using Nimbus.Handlers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScale.Movies.DataService.Handlers
{
    public class MovieScoreHandler : IHandleMulticastEvent<NewScoreEvent>
    {
        private readonly IHubContext movieHub;

        public MovieScoreHandler()
        {
            this.movieHub = GlobalHost.ConnectionManager.GetHubContext<MovieHub>();
        }

        public async Task Handle(NewScoreEvent busEvent)
        {
            await movieHub.Clients.All.Notify(busEvent.UserId + " just scored " + busEvent.MovieId + " " + busEvent.Score);
        }
    }
}
