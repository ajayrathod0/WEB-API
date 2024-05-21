using Newtonsoft.Json;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CRUDUsingEF
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.MessageHandlers.Add(new TokenValidationHandler());

            // Web API configuration and services

            EnableCorsAttribute corsAttribute = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttribute); //enable cross origin resource shareing

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
                = ReferenceLoopHandling.Ignore;
        }
    }
}
