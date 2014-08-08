using Autofac;
using System.Collections.Generic;
using System.Linq;
using System;
using Serilog;
using System.Threading;

namespace CloudScale.Airline.FlightService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                                    .WriteTo.ColoredConsole()
                                    .CreateLogger();

            // Create your container
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(typeof(Program).Assembly);
            builder.RegisterType<FlightService>();

            var container = builder.Build();

            FlightService service = container.Resolve<FlightService>();
            service.Start();

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
