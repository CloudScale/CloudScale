using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure.ServiceRuntime;
using Autofac;
using Serilog;

namespace CloudScale.Movies.LookupService
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.TraceInformation("CloudScale.Movies.LookupService entry point called");

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(WorkerRole).Assembly);
            var container = builder.Build();

            Thread.Sleep(Timeout.Infinite);
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            Log.Logger = new LoggerConfiguration()
                                    .WriteTo.ColoredConsole()
                                    .WriteTo.Trace()
                                    .CreateLogger();

            return base.OnStart();
        }
    }
}
