using Microsoft.AspNet.SignalR;

namespace CloudScale.SignalR.Hubs
{
    public class NotificationHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}