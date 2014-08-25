using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using CloudScale.Api.Controllers;
using CloudScale.Api.Filters;

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