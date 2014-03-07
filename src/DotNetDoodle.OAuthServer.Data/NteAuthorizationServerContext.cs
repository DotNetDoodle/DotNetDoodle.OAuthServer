using DotNetDoodle.OAuthServer.Data.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;

namespace DotNetDoodle.OAuthServer.Data
{
    public class OAuthServerContext : DbContext
    {
        public OAuthServerContext() : base("OAuthServerContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

#if DEBUG
            base.Database.Log = (msg) =>
            {
                Trace.TraceInformation(msg);
            };
#endif
        }

        public virtual DbSet<ApplicationEntity> Applications { get; set; }
        public virtual DbSet<ScopeEntity> ApplicationScopes { get; set; }
        public virtual DbSet<ClientEntity> Clients { get; set; }
        public virtual DbSet<ClientRedirectUriEntity> RedirectUris { get; set; }
        public virtual DbSet<StoredGrantEntity> StoredGrants { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.RemoveSuffixFromTheTableNames("Entity");
        }
    }
}
