using System;
using System.Diagnostics;
using Autofac;

namespace CloudScale.Movies.LookupService
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
			Trace.TraceInformation("CloudScale.Movies.LookupService Start");
		}

		public void Stop()
		{
			Trace.TraceInformation("CloudScale.Movies.LookupService Stop");
		}
	}
}