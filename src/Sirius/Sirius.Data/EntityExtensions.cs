using System.Linq;
using Sirius.Shared.Entities;

namespace Sirius.Data
{
    public static class EntityExtensions
    {
        public static IQueryable<TEntity> Exists<TEntity>(this IQueryable<TEntity> query)
            where TEntity : class, IEntity
        {
            return query.Where(p => !p.IsDeleted);
        }

        public static bool Delete<TEntity>(this TEntity entity)
            where TEntity : IEntity
        {
            if (entity.IsDeleted) return false;
            entity.IsDeleted = true;
            return true;
        }
    }
}
