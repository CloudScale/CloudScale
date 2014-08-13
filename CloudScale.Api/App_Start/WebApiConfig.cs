using CloudScale.Api.Filters;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Cors;
using System.Web.Http;

namespace CloudScale.Api
{
    public static class WebApiConfig
    {
        public static void Register(IAppBuilder app, HttpConfiguration config)
        {
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.MapHttpAttributeRoutes();
            config.EnableCors();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            //config.Filters.Add(new MyAuthenticationFilter());

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }
    }
}
