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
            AutofacConfig.Register(app);
            SignalRConfig.Register(app);
        }
    }
}
