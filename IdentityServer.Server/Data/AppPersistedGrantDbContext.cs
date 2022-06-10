using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Server.Data
{
    public class AppPersistedGrantDbContext : PersistedGrantDbContext<AppPersistedGrantDbContext>
    {
        public AppPersistedGrantDbContext(DbContextOptions options, OperationalStoreOptions storeOptions) : base(options, storeOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var table = entity.GetTableName();
                if (table != null)
                    entity.SetTableName("__IdentityServerPersistedGrant__" + table);
            }
        }
    }
}
