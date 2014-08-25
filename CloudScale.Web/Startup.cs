using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CloudScale.Web;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof (Startup))]

namespace CloudScale.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.Register(GlobalFilters.Filters);
            RouteConfig.Register(RouteTable.Routes);
            BundleConfig.Register(BundleTable.Bundles);
        }
    }
}