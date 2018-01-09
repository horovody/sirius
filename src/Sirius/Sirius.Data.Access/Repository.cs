using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Sirius.Shared;
using Sirius.Shared.Entities;

namespace Sirius.Data.Access
{
    internal class Repository : IRepository
    {
        private readonly ILifetimeScope _container;

        public Repository(ILifetimeScope container)
        {
            _container = container;
        }

        #region Implementation of IRepository

        public IStore<TEntity> Store<TEntity>() where TEntity : class, IEntity
        {
            return _container.Resolve<IStore<TEntity>>();
        }

        public async Task<TEntity> GetAsync<TEntity>(Guid id, CancellationToken cancellationToken) where TEntity : class, IEntity
        {
            return await this.Store<TEntity>().GetAsync(id, cancellationToken);
        }

        public async Task<TEntity> GetExistsAsync<TEntity>(Guid id, CancellationToken cancellationToken) where TEntity : class, IEntity
        {
            var entity = await this.Store<TEntity>().GetAsync(id, cancellationToken);
            if (entity == null || entity.IsDeleted) return null;
            return entity;
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class, IEntity
        {
            return this.Store<TEntity>().Query();
        }

        public IQueryable<TEntity> Exists<TEntity>() where TEntity : class, IEntity
        {
            return this.Store<TEntity>().Query().Exists();
        }

        public async Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class, IEntity
        {
            return await this.Store<TEntity>().AddAsync(entity, cancellationToken);
        }

        public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class, IEntity
        {
            return await this.Store<TEntity>().UpdateAsync(entity, cancellationToken);
        }

        public async Task<TEntity> DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class, IEntity
        {
            entity.IsDeleted = true;
            return await this.Store<TEntity>().UpdateAsync(entity, cancellationToken);
        }

        #endregion
    }
}
