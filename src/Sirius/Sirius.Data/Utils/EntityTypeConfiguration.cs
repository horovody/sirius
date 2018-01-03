using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sirius.Shared.Entities;

namespace Sirius.Data.Utils
{
    public abstract class EntityTypeConfiguration<TEntity>
        where TEntity : class, IEntity
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}
