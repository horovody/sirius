using System.Linq;
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

        public async Task<TEntity> Get<TEntity>(long id) where TEntity : class, IEntity
        {
            return await this.Store<TEntity>().Get(id);
        }

        public async Task<TEntity> GetExists<TEntity>(long id) where TEntity : class, IEntity
        {
            var entity = await this.Store<TEntity>().Get(id);
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

        public async Task<TEntity> Add<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            return await this.Store<TEntity>().Add(entity);
        }

        public async Task<TEntity> Update<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            return await this.Store<TEntity>().Update(entity);
        }

        public async Task<TEntity> Delete<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            entity.IsDeleted = true;
            return await this.Store<TEntity>().Update(entity);
        }

        #endregion
    }
}
