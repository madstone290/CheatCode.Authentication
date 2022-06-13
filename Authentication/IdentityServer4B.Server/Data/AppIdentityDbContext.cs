using IdentityServer4B.Server.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer4B.Server.Data
{
    public class AppIdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>

    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach(var entity in builder.Model.GetEntityTypes())
            {
                var table = entity.GetTableName();
                if (table != null)
                    entity.SetTableName("__Identity__" + table);
            }
        }
    }
}
