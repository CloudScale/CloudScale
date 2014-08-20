using Autofac.Integration.WebApi;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using StatsdClient;

namespace CloudScale.Api.Filters
{
    public class StatsdActionFilter : IAutofacActionFilter
    {
        private readonly IStatsd statsd;
        private readonly Stopwatch watch = new Stopwatch();

        public StatsdActionFilter(IStatsd statsd)
        {
            this.statsd = statsd;
        }

        public void OnActionExecuted(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {
            watch.Stop();
            if (statsd != null)
            {
                statsd.LogCount("action." + actionExecutedContext.ActionContext.ActionDescriptor.ActionName.ToLower());
                statsd.LogTiming("action." + actionExecutedContext.ActionContext.ActionDescriptor.ActionName.ToLower() + ".timing", watch.Elapsed);
            }
        }

        public void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            watch.Start();
        }
    }
}