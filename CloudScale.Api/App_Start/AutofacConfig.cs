using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;

namespace CloudScale.Api
{
    public static class AutofacConfig
    {
        public static void Register(IAppBuilder app, HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(typeof (AutofacConfig).Assembly);

            IContainer container = builder.Build();

            var dependencyResolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = dependencyResolver;

            GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(GlobalConfiguration.Configuration);
        }
    }
}