using Autofac;
using DotNetDoodle.OAuthServer.Managers.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetDoodle.OAuthServer.Infrastructure.DependencyInjection
{
    public class ManagerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            const string ManagerSuffix = "Manager";
            Type managerBaseType = typeof(ManagerBase);
            List<Type> managerTypes = managerBaseType.Assembly.GetTypes().Where(type => type != managerBaseType && managerBaseType.IsAssignableFrom(type)).ToList();
            managerTypes.ForEach(managerType =>
            {
                Type managerInterface = managerType.GetInterfaces().Where(x => x.Name.EndsWith(ManagerSuffix, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                builder.RegisterType(managerType).As(managerInterface).InstancePerLifetimeScope();
            });
        }
    }
}