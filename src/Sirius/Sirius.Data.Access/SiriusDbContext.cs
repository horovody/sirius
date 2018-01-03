using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sirius.Data.Entities;
using Sirius.Shared;
using Sirius.Shared.Entities;

namespace Sirius.Data.Access
{
    public class SiriusDbContext: IdentityDbContext, IUnitOfWork
    {
        private readonly IConfiguration _config;

        public SiriusDbContext()
        {
        }

        public SiriusDbContext(IConfiguration config)
        {
            _config = config;
        }

        public SiriusDbContext(DbContextOptions<SiriusDbContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            {
                if (optionsBuilder.IsConfigured == false)
                {
                    var connectionString = _config.GetConnectionString("SiriusConnection");
                    optionsBuilder.UseNpgsql(connectionString);
                }
                base.OnConfiguring(optionsBuilder);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        #region Entities

        public DbSet<SettingEntity> Settings { get; set; }

        #endregion

        #region Implementation of IUnitOfWork

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        #endregion

        #region Audit Rules

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker.Entries()
                .Where(
                    e => (e.Entity is IUpdatedEntity || e.Entity is ICreatedEntity) && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var created = entry.Entity as ICreatedEntity;
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (created != null)
                        {
                            created.Created = DateTime.UtcNow;
                        }
                        break;
                    case EntityState.Modified:
                        var updated = entry.Entity as IUpdatedEntity;
                        if (updated != null)
                        {
                            updated.Updated = DateTime.UtcNow;
                        }
                        break;
                }
            }
        }

        #endregion
    }
}
