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