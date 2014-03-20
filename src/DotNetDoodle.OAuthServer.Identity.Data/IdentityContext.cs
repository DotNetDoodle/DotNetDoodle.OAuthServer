using DotNetDoodle.OAuthServer.Identity.Data.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;

namespace DotNetDoodle.OAuthServer.Identity.Data
{
    public class IdentityContext : IdentityDbContext<UserEntity>
    {
        public IdentityContext() : this("IdentityContext")
        {
        }

        public IdentityContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
#if DEBUG
            Database.Log = (msg) => Trace.TraceInformation(msg);
#endif
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.RemoveSuffixFromTheTableNames("Entity");

            modelBuilder.Entity<UserEntity>().Property(c => c.UserName).HasMaxLength(50);
        }
    }
}
