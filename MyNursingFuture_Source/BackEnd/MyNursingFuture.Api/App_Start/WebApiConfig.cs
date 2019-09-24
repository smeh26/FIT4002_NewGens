using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Tracing;
using System.Diagnostics.Tracing;

namespace MyNursingFuture.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var origin = ConfigurationManager.AppSettings["cors.origin"];
            var allow = new EnableCorsAttribute(origin, "*", "*");
           // config.EnableSystemDiagnosticsTracing();
            
            // Web API configuration and services
            config.EnableCors(allow);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
