using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;

namespace DotNetDoodle.OAuthServer.Infrastructure.DependencyInjection
{
    using Module = Autofac.Module;

    public class WebApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }
    }
}