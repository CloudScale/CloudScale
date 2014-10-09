using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using CloudScale.Movies.Messages;
using Metrics;
using Metrics.Core;
using Metrics.Reporters;
using Metrics.Reports;
using Nimbus;
using Serilog;

namespace CloudScale.Movies.Simulator
{
	internal class Program
	{
		private static readonly Counter counter = Metric.Counter("test_counter", Unit.Calls);

		private static void Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
				.WriteTo.ColoredConsole()
				.CreateLogger();

			Metric.Config
				.WithReporting(p =>
				{
					p.WithReporter("Cool", () => new StringReporter(), TimeSpan.FromSeconds(3));
					p.WithConsoleReport(TimeSpan.FromSeconds(5));
				});

			var builder = new ContainerBuilder();
			builder.RegisterAssemblyModules(typeof(Program).Assembly);
			IContainer container = builder.Build();

			var bus = container.Resolve<IBus>();

			while (true)
			{
				Console.WriteLine("p - ping");
				Console.WriteLine("h - healthcheck");
				Console.WriteLine("q - quit");
				Console.WriteLine();
				Console.Write(":> ");

				string readLine = Console.ReadLine();

				counter.Increment();

				if (readLine.Trim().Equals("p", StringComparison.InvariantCultureIgnoreCase))
				{
					Task<IEnumerable<PingResponse>> responses = bus.MulticastRequest(new PingRequest(),
						TimeSpan.FromSeconds(5));
					responses.Wait();

					foreach (PingResponse pingResponse in responses.Result)
					{
						Log.Information("Received Response: {From}", pingResponse.Details);
					}

					Console.WriteLine("...");
				}
				else if (readLine.Trim().Equals("h", StringComparison.InvariantCultureIgnoreCase))
				{
					Log.Information("Metrics = {@Metrics}", HealthChecks.GetStatus());
				}
				else if (readLine.Trim().Equals("q", StringComparison.InvariantCultureIgnoreCase))
				{
					Environment.Exit(0);
				}
			}
		}
	}
}