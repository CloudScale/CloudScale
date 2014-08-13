using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.IO;
using System.Web;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(CloudScale.Api.Startup))]
namespace CloudScale.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            AutofacConfig.Register(app, config);
            OAuthConfig.Register(app, config);
            WebApiConfig.Register(app, config);
        }
    }
}
