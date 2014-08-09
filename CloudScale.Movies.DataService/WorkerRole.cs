using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure.ServiceRuntime;
using Serilog;
using Autofac;
using System.Data.Entity;
using FluentScheduler;
using CloudScale.Movies.DataService.Scheduler;

namespace CloudScale.Movies.DataService
{
    public class WorkerRole : RoleEntryPoint
    {
        private IContainer _container;
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.TraceInformation("CloudScale.Movies.DataService entry point called");

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(WorkerRole).Assembly);
            _container = builder.Build();

            TaskManager.TaskFactory = _container.Resolve<ITaskFactory>();
            TaskManager.Initialize(_container.Resolve<DataImportRegistry>());

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

        public override void OnStop()
        {
            base.OnStop();
        }
    }
}
