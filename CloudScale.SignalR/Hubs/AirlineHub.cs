using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace CloudScale.SignalR.Hubs
{
    public class AirlineHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}