using Microsoft.AspNet.SignalR;

namespace CloudScale.SignalR.Hubs
{
    public class MovieHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}