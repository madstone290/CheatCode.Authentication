using EFCore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Core.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;

        public IList<string> Watcher { get; set; } = new List<string>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserDbContext).Assembly);
        }

        public override int SaveChanges()
        {
            Watcher.Add("SaveChanges()");
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            Watcher.Add("SaveChanges(bool acceptAllChangesOnSuccess)");
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            Watcher.Add("SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)");
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            Watcher.Add("SaveChangesAsync(CancellationToken cancellationToken = default)");
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
