using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CloudScale.Web.Startup))]
namespace CloudScale.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
