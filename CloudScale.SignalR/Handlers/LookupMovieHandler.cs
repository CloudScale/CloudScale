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
    public class LookupMovieHandler : IHandleMulticastEvent<LookupMovieEvent>
    {
        private readonly IHubContext movieHub;

        public LookupMovieHandler()
        {
            this.movieHub = GlobalHost.ConnectionManager.GetHubContext<MovieHub>();
        }

        public async Task Handle(LookupMovieEvent busEvent)
        {
            await movieHub.Clients.All.Notify("New data is available for " + busEvent.Name); 
        }
    }
}
