using System;
using System.Reflection;
using Autofac;
using CloudScale.Movies.Messages;
using Microsoft.WindowsAzure;
using Nimbus;
using Nimbus.Configuration;
using Nimbus.Infrastructure;
using Nimbus.Logger.Serilog;
using Module = Autofac.Module;

namespace CloudScale.SignalR.AutofacModules
{
    public class BusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            string connectionString = CloudConfigurationManager.GetSetting("ServiceBusConnectionString");

            // You'll want a logger. There's a ConsoleLogger and a NullLogger if you really don't care. You can roll your
            // own by implementing the ILogger interface if you want to hook it to an existing logging implementation.
            builder.RegisterType<SerilogStaticLogger>()
                .AsImplementedInterfaces()
                .SingleInstance();

            // This is how you tell Nimbus where to find all your message types and handlers.
            Assembly moviesAssembly = typeof (PingRequest).Assembly;
            Assembly nimbusAssembly = typeof (Bus).Assembly; // for stock interceptors

            var handlerTypesProvider = new AssemblyScanningTypeProvider(ThisAssembly, nimbusAssembly, moviesAssembly);

            builder.RegisterNimbus(handlerTypesProvider);

            builder.Register(componentContext => new BusBuilder()
                .Configure()
                .WithConnectionString(connectionString)
                .WithNames("CloudScale.SignalR", Environment.MachineName)
                .WithTypesFrom(handlerTypesProvider)
                .WithAutofacDefaults(componentContext)
                .Build())
                .As<IBus>()
                .AutoActivate()
                .OnActivated(c => c.Instance.Start())
                .SingleInstance();
        }
    }
}