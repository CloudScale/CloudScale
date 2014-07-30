using Autofac;
using System.Collections.Generic;
using System.Linq;
using Topshelf.Autofac;
using System;
using Topshelf;
using Nimbus;
using Serilog;

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

            //object newVariable = s =>
            //                {
            //                    // Let Topshelf use it
            //                    s.ConstructUsingAutofacContainer();
            //                    s.WhenStarted((service, control) => service.Start());
            //                    s.WhenStopped((service, control) => service.Stop());
            //                };

            HostFactory.Run(c =>
            {
                c.UseAutofacContainer(container);

                c.Service<FlightService>(s =>
                {
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted(t => t.Start());
                    s.WhenStopped(t => t.Stop());
                });

                c.RunAsLocalSystem();
                c.StartAutomatically();
            });
        }
    }
}
