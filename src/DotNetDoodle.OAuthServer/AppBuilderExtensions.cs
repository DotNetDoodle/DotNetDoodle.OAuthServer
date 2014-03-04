using DotNetDoodle.OAuthServer.Infrastructure.Config;
using DotNetDoodle.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
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
            config.Routes.MapHttpRoute("OAuthAuthorizeRoute", "oauth/authorize", new { controller = "authorize" });
            config.Routes.MapHttpRoute("OAuthLoginRoute", "login", new { controller = "login" });

            return app.UseWebApiWithContainer(config);
        }

        public static IAppBuilder RunOAuthServer(this IAppBuilder app, IConfigurationManager configManager)
        {
            OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions 
            {
                AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(configManager.AccessTokenTimeoutInSeconds)
            };

            return app.UseOAuthAuthorizationServer(oAuthServerOptions);
        }
    }
}