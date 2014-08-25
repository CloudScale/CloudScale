using Autofac;
using FluentScheduler;
using Serilog;

namespace CloudScale.Movies.DataService.Scheduler
{
    public class AutofacTaskFactory : ITaskFactory
    {
        private readonly ILifetimeScope scope;

        /// <summary>
        ///     Initializes a new instance of the AutofacTaskFactory class.
        /// </summary>
        public AutofacTaskFactory(ILifetimeScope scope)
        {
            this.scope = scope;
        }

        public ITask GetTaskInstance<T>() where T : ITask
        {
            Log.Information("AutofacTaskFactory() trying to resolve {Type}", typeof (T).FullName);

            return scope.Resolve<T>();
        }
    }
}