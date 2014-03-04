using Autofac;
using DotNetDoodle.OAuthServer.Infrastructure.Config;

namespace DotNetDoodle.OAuthServer.Infrastructure.DependencyInjection
{
    public class ConfigurationManagerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnifiedConfigurationManager>().As<IConfigurationManager>().SingleInstance();
        }
    }
}