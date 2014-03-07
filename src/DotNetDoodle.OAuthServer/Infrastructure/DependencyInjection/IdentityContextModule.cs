using Autofac;
using DotNetDoodle.OAuthServer.Identity.Data;

namespace DotNetDoodle.OAuthServer.Infrastructure.DependencyInjection
{
    public class IdentityContextModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IdentityContext>().InstancePerLifetimeScope();
        }
    }
}