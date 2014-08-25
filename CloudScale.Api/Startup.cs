using System.Web.Http;
using CloudScale.Api;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof (Startup))]

namespace CloudScale.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            AutofacConfig.Register(app, config);
            OAuthConfig.Register(app, config);
            WebApiConfig.Register(app, config);
        }
    }
}