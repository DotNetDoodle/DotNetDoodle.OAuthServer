using Autofac;
using DotNetDoodle.OAuthServer.Identity.Data;
using DotNetDoodle.OAuthServer.Identity.Data.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DotNetDoodle.OAuthServer.Infrastructure.DependencyInjection
{
    public class IdentityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new UserStore<UserEntity>(c.Resolve<IdentityContext>())).As<IUserStore<UserEntity>>().InstancePerLifetimeScope();
            builder.RegisterType<UserManager<UserEntity>>().InstancePerLifetimeScope();
        }
    }
}