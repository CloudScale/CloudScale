using System;
using System.Diagnostics;
using Autofac;
using CloudScale.Movies.DataService.Scheduler;
using FluentScheduler;

namespace CloudScale.Movies.DataService
{
	public class ServiceImplementation : IService
	{
		private readonly ILifetimeScope scope;

		public ServiceImplementation(ILifetimeScope scope)
		{
			if (scope == null) throw new ArgumentNullException("scope");

			this.scope = scope;
		}

		public void Start()
		{
			Trace.TraceInformation("CloudScale.Movies.DataService Start");

			TaskManager.TaskFactory = scope.Resolve<ITaskFactory>();
			TaskManager.Initialize(scope.Resolve<DataImportRegistry>());
		}

		public void Stop()
		{
			Trace.TraceInformation("CloudScale.Movies.DataService Stop");
		}
	}
}