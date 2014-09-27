using Autofac;
using Serilog;
using Topshelf;
using Topshelf.Autofac;

namespace CloudScale.Movies.DataService
{
	public class Program
	{
		static void Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
						.WriteTo.ColoredConsole()
						.Enrich.WithProcessId()
						.CreateLogger();

			var builder = new ContainerBuilder();
			builder.RegisterAssemblyModules(typeof(Program).Assembly);
			IContainer container = builder.Build();

			HostFactory.Run(x =>
			{
				x.UseAutofacContainer(container);
				x.Service<ServiceImplementation>(s =>
				{
					s.ConstructUsingAutofacContainer();
					s.WhenStarted(ns => ns.Start());
					s.WhenStopped(ns => ns.Stop());
				});
				x.RunAsLocalSystem();
				x.SetDescription("Data Service");
				x.SetDisplayName("Data Service");
				x.SetServiceName("DataService");
			});
		}
	}
}