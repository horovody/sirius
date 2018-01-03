using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sirius.Shared.Entities;

namespace Sirius.Shared
{
    public interface IStore<TEntity>
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> Query();
        IQueryable<TEntity> Query<TProperty>(Expression<Func<TEntity, TProperty>> includeProperty);
        IQueryable<TEntity> Query(Expression<Func<TEntity, object>>[] includeProperties);

        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> Get(long id);

        Task<TEntity> Add(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task<TEntity> Remove(TEntity entity);
        Task<TEntity> Remove(long id);
        Task Remove(Expression<Func<TEntity, bool>> predicate);
    }
}
