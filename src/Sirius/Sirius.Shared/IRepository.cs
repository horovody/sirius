using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sirius.Shared.Entities;

namespace Sirius.Shared
{
    public interface IRepository
    {
        IStore<TEntity> Store<TEntity>() where TEntity : class, IEntity;
        Task<TEntity> GetAsync<TEntity>(Guid id, CancellationToken cancellationToken) where TEntity : class, IEntity;
        Task<TEntity> GetExistsAsync<TEntity>(Guid id, CancellationToken cancellationToken) where TEntity : class, IEntity;
        IQueryable<TEntity> Query<TEntity>() where TEntity : class, IEntity;
        IQueryable<TEntity> Exists<TEntity>() where TEntity : class, IEntity;
        Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class, IEntity;
        Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class, IEntity;
        Task<TEntity> DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class, IEntity;
    }
}
