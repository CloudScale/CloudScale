using Autofac;
using Autofac.Integration.WebApi;
using Nimbus;
using Nimbus.Configuration;
using Nimbus.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudScale.Api.AutofacModules
{
    public class WebApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Register the Web API controllers.
            builder.RegisterApiControllers(ThisAssembly);
        }
    }
}
