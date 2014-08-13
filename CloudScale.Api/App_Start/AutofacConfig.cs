using Autofac;
using Autofac.Integration.WebApi;
using Autofac.Integration.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Web.Http;
using Autofac.Core;

namespace CloudScale.Api
{

    public static class AutofacConfig
    {
        public static void Register(IAppBuilder app, HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(typeof(AutofacConfig).Assembly);
            
            var container = builder.Build();

            var dependencyResolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = dependencyResolver;
            
            GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(GlobalConfiguration.Configuration);
        }
    }
}
