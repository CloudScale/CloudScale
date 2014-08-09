using Autofac;
using Autofac.Integration.SignalR;
using Microsoft.AspNet.SignalR;

namespace CloudScale.SignalR
{
    public static class ContainerConfig
    {
        public static void Configure()
        {
            // Create the container builder.
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(typeof(ContainerConfig).Assembly);

            var container = builder.Build();

            // Create the depenedency resolver.
            var resolver = new AutofacDependencyResolver(container);

            // Configure SignalR with the dependency resolver.
            GlobalHost.DependencyResolver = resolver;
        }
    }
}
