using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
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
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken);

        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);

        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        Task<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> RemoveAsync(Guid id, CancellationToken cancellationToken);
        Task RemoveAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    }
}
