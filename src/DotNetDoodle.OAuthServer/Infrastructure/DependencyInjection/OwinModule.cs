using Autofac;
using DotNetDoodle.Owin.Dependencies.Autofac;

namespace DotNetDoodle.OAuthServer.Infrastructure.DependencyInjection
{
    public class OwinModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterOwinApplicationContainer();
        }
    }
}