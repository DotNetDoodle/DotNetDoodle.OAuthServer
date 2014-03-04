using DotNetDoodle.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Web.Http;
using WebApiContrib.Formatting.Razor;

namespace DotNetDoodle.OAuthServer
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder RunWebApi(this IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            // Configure formatters
            config.Formatters.Clear();
            config.Formatters.Add(new RazorViewFormatter());

            // Configure routes
            config.Routes.MapHttpRoute("OAuthAuthorizeRoute", "oauth/authorize");

            return app.UseWebApiWithContainer(config);
        }

        public static IAppBuilder RunOAuthServer(this IAppBuilder app)
        {
            OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions 
            {
            };

            return app.UseOAuthAuthorizationServer(oAuthServerOptions);
        }
    }
}