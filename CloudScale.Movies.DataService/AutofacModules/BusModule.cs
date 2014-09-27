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

namespace AutofacModules
{
	public class BusModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");

			builder.RegisterType<SerilogStaticLogger>()
				.AsImplementedInterfaces()
				.SingleInstance();

			// This is how you tell Nimbus where to find all your message types and handlers.
			Assembly messagesAssembly = typeof (PingRequest).Assembly;
			Assembly nimbusAssembly = typeof (Bus).Assembly; // for stock interceptors

			var handlerTypesProvider = new AssemblyScanningTypeProvider(ThisAssembly, nimbusAssembly, messagesAssembly);

			builder.RegisterNimbus(handlerTypesProvider);
			builder.Register(componentContext => new BusBuilder()
				.Configure()
				.WithConnectionString(connectionString)
				.WithNames("CloudScale.Movies.DataService", Environment.MachineName)
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