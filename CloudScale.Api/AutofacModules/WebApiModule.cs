using Autofac;
using Autofac.Integration.WebApi;
using CloudScale.Api.Filters;
using Nimbus;
using Nimbus.Configuration;
using Nimbus.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CloudScale.Api.Controllers;

namespace CloudScale.Api.AutofacModules
{
    public class WebApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<OAuthClaimsAuthenticationFilter>()
                    .AsWebApiAuthenticationFilterFor<MoviesController>()
                    .InstancePerRequest();

            builder.RegisterType<StatsdActionFilter>()
                    .AsWebApiActionFilterFor<MoviesController>()
                    .InstancePerRequest();

            // Register the Web API controllers.
            builder.RegisterApiControllers(ThisAssembly).InstancePerRequest();
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
        }
    }
}
