using Autofac;
using Autofac.Integration.WebApi;
using CloudScale.Api.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

namespace CloudScale.Api
{
    public static class OAuthConfig
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions GoogleAuthOptions { get; private set; }
        public static FacebookAuthenticationOptions FacebookAuthOptions { get; private set; }
        public static string FacebookAppToken { get; private set; }

        public static void Register(IAppBuilder app, HttpConfiguration config)
        {
            //app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            
            //GoogleAuthOptions = new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "xxx",
            //    ClientSecret = "xxx",
            //    Provider = new GoogleAuthProvider()
            //};
            //app.UseGoogleAuthentication(GoogleAuthOptions);

            //FacebookAppToken = "xxx";
            //FacebookAuthOptions = new FacebookAuthenticationOptions()
            //{
            //    AppId = "xxx",
            //    AppSecret = "xxx",
            //    Provider = new FacebookAuthProvider()
            //};
            //app.UseFacebookAuthentication(FacebookAuthOptions);

            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                ApplicationCanDisplayErrors = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = (IOAuthAuthorizationServerProvider)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(SimpleAuthorizationServerProvider)),
                RefreshTokenProvider = (IAuthenticationTokenProvider)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(SimpleRefreshTokenProvider))
            };
          
            // Token Generation
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
