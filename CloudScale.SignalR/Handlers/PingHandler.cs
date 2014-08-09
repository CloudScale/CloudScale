using CloudScale.Movies.Messages;
using CloudScale.SignalR.Hubs;
using Microsoft.AspNet.SignalR;
using Nimbus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScale.SignalR.Handlers
{
    public class PingHandler : IHandleRequest<PingRequest, PingResponse>
    {
        private readonly IHubContext movieHub;

        public PingHandler()
        {
            this.movieHub = GlobalHost.ConnectionManager.GetHubContext<MovieHub>();
        }

        public async Task<PingResponse> Handle(PingRequest request)
        {
            movieHub.Clients.All.Ping(); 

            return await Task.Run(() => { return new PingResponse() { Details = this.GetType().FullName }; });
        }
    }
}
