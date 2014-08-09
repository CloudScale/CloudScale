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
    public class BusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var connectionString = Microsoft.WindowsAzure.CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");

            // You'll want a logger. There's a ConsoleLogger and a NullLogger if you really don't care. You can roll your
            // own by implementing the ILogger interface if you want to hook it to an existing logging implementation.
            builder.RegisterType<Nimbus.Logger.Serilog.SerilogStaticLogger>()
                   .AsImplementedInterfaces()
                   .SingleInstance();

            // This is how you tell Nimbus where to find all your message types and handlers.
            var airlineAssemlbly = typeof(CloudScale.Airline.Messages.NewFlightCommand).Assembly;
            var moviesAssembly = typeof(CloudScale.Movies.Messages.PingRequest).Assembly;
            var nimbusAssembly = typeof(Bus).Assembly; // for stock interceptors

            var handlerTypesProvider = new AssemblyScanningTypeProvider(ThisAssembly, nimbusAssembly, airlineAssemlbly, moviesAssembly);
            
            builder.RegisterNimbus(handlerTypesProvider);

            builder.Register(componentContext => new BusBuilder()
                                 .Configure()
                                 .WithConnectionString(connectionString)
                                 .WithNames("CloudScale.Api", Environment.MachineName)
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