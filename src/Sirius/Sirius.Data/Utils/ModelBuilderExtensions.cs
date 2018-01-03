using Microsoft.EntityFrameworkCore;
using Sirius.Shared.Entities;

namespace Sirius.Data.Utils
{
    public static class ModelBuilderExtensions
    {
        public static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder, EntityTypeConfiguration<TEntity> configuration)
            where TEntity : class, IEntity
        {
            configuration.Map(modelBuilder.Entity<TEntity>());
        }
    }
}
