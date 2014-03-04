using DotNetDoodle.OAuthServer.Infrastructure.Config;
using DotNetDoodle.OAuthServer.Infrastructure.Providers;
using DotNetDoodle.Owin;
using Microsoft.Owin;
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
#if DEBUG
                AllowInsecureHttp = true,
#endif
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(configManager.AccessTokenTimeoutInSeconds),
                Provider = new DotNetDoodleOAuthAuthorizationServerProvider(app, configManager)
            };

            return app.UseOAuthAuthorizationServer(oAuthServerOptions);
        }
    }
}