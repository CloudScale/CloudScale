using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using CloudScale.Movies.Messages;
using Nimbus;
using Serilog;

namespace CloudScale.Movies.Simulator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .CreateLogger();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof (Program).Assembly);
            IContainer container = builder.Build();

            var bus = container.Resolve<IBus>();

            while (true)
            {
                Console.WriteLine("Hit Enter to send Ping");

                string readLine = Console.ReadLine();

                if (string.IsNullOrEmpty(readLine))
                {
                    Task<IEnumerable<PingResponse>> responses = bus.MulticastRequest<PingRequest, PingResponse>(new PingRequest(),
                        TimeSpan.FromSeconds(5));
                    responses.Wait();

                    foreach (PingResponse pingResponse in responses.Result)
                    {
                        Log.Information("Received Response: {From}", pingResponse.Details);
                    }

                    Console.WriteLine("...");
                }
            }
        }
    }
}