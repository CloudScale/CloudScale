using Autofac;
using Nimbus;
using Nimbus.Configuration;
using Nimbus.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nimbus.Logger.Serilog;
using CloudScale.Movies.DataService;
using FluentScheduler;
using FluentScheduler.Extensions;
using Serilog;

namespace CloudScale.Movies.DataService.Scheduler
{
    public class AutofacTaskFactory : ITaskFactory
    {
        private readonly ILifetimeScope scope;
        /// <summary>
        /// Initializes a new instance of the AutofacTaskFactory class.
        /// </summary>
        public AutofacTaskFactory(ILifetimeScope scope)
        {
            this.scope = scope;
        }

        public ITask GetTaskInstance<T>() where T : ITask
        {
            Log.Information("AutofacTaskFactory() trying to resolve {Type}", typeof(T).FullName);

            return scope.Resolve<T>();
        }
    }
}
