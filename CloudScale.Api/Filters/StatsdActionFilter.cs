using System.Diagnostics;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;
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

        public void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            watch.Stop();
            if (statsd != null)
            {
                statsd.LogCount("action." + actionExecutedContext.ActionContext.ActionDescriptor.ActionName.ToLower());
                statsd.LogTiming(
                    "action." + actionExecutedContext.ActionContext.ActionDescriptor.ActionName.ToLower() + ".timing",
                    watch.Elapsed);
            }
        }

        public void OnActionExecuting(HttpActionContext actionContext)
        {
            watch.Start();
        }
    }
}