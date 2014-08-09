using System;
using Owin;
using Autofac;
using Autofac.Integration.SignalR;
using System.Reflection;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;

namespace CloudScale.SignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ContainerConfig.Configure();

            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCors(CorsOptions.AllowAll);

            app.MapSignalR();
        }
    }
}
