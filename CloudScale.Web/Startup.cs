using Microsoft.Owin;
using Owin;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: OwinStartupAttribute(typeof(CloudScale.Web.Startup))]
namespace CloudScale.Web
{
    public partial class Startup
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
