using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Integration.SignalR;

namespace CloudScale.SignalR.AutofacModules
{
    public class SignalRModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Register the SignalR hubs.
            
            builder.RegisterHubs(ThisAssembly);
        }
    }
}
