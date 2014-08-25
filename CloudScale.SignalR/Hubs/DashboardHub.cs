using System;
using Microsoft.AspNet.SignalR;

namespace CloudScale.SignalR.Hubs
{
    public class DashboardHub : Hub, IDashboardHub
    {
        private readonly int value1;
        private readonly string value2;

        public DashboardHub(int value1, string value2)
        {
            this.value1 = value1;
            this.value2 = value2;
        }

        public void Hello()
        {
            Clients.All.hello();
        }


        public void Test()
        {
            if (true)
            {
                Console.WriteLine("hi");
            }
        }
    }

    public interface IDashboardHub
    {
        void Hello();
        void Test();
    }
}