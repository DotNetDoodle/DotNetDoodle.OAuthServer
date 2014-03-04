using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetDoodle.Owin.Dependencies;
using DotNetDoodle.Owin.Dependencies.Autofac;
using Microsoft.Owin.Security.OAuth;
using DotNetDoodle.Owin;
using Autofac;
using Microsoft.Owin.Logging;
using System.Reflection;
using DotNetDoodle.OAuthServer.Infrastructure.DependencyInjection;

namespace DotNetDoodle.OAuthServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ILogger logger = app.CreateLogger<Startup>();
            logger.WriteInformation("OAuth Server is starting up.");

            try
            {
                IContainer container = AutofacConfig.InitializeContainer();
                app.UseAutofacContainer(container)
                    .RunOAuthServer()
                    .RunWebApi();
            }
            catch (Exception ex)
            {
                logger.WriteError("OAuth Server could not be registered correctly.", ex);
                throw;
            }

            logger.WriteInformation("OAuth Server is successfully configured.");
        }
    }
}