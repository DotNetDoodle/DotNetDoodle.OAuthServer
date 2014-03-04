using Autofac;
using DotNetDoodle.OAuthServer.Infrastructure.Config;
using DotNetDoodle.OAuthServer.Infrastructure.DependencyInjection;
using DotNetDoodle.Owin;
using Microsoft.Owin.Logging;
using Owin;
using System;

namespace DotNetDoodle.OAuthServer
{
    public class Startup
    {
        private readonly IContainer _container;

        public Startup()
        {
        }

        public Startup(IContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
        }

        public void Configuration(IAppBuilder app)
        {
            ILogger logger = app.CreateLogger<Startup>();
            logger.WriteInformation("OAuth Server is starting up.");

            try
            {
                IContainer container = (_container != null) ? _container : AutofacConfig.InitializeContainer();
                IConfigurationManager configManager = container.Resolve<IConfigurationManager>();
                if (configManager == null)
                {
                    throw new InvalidOperationException("OAuth server could not fuction without an IConfigurationManager implementation. You need to register one through the IoC container.");
                }

                app.UseAutofacContainer(container)
                    .RunOAuthServer(configManager)
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