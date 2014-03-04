using Autofac;
using System.Reflection;

namespace DotNetDoodle.OAuthServer.Infrastructure.DependencyInjection
{
    public static class AutofacConfig
    {
        public static IContainer InitializeContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

            return builder.Build();
        }
    }
}