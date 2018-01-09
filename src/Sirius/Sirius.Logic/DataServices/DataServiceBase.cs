using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sirius.Shared;
using Sirius.Shared.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Sirius.Logic.DataServices
{
    internal class DataServiceBase<TEntity, TModel> : ICrudDataService<TEntity, TModel>
        where TEntity : class, IEntity
        where TModel : class, IEntityTransferObject
    {
        public DataServiceBase(IStore<TEntity> store, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.Store = store;
            this.Mapper = mapper;
            this.UnitOfWork = unitOfWork;
        }

        protected IStore<TEntity> Store { get; }

        protected IMapper Mapper { get; set; }

        protected IUnitOfWork UnitOfWork { get; set; }

        public virtual async Task<TModel> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await this.Store.GetAsync(id, cancellationToken);
            return this.Mapper.Map<TEntity, TModel>(entity);
        }

        protected IQueryable<TEntity> QueryInternal()
        {
            return this.Store.Query()
                .Where(p => !p.IsDeleted);
        }

        protected virtual IQueryable<TModel> GetQuery(IQueryable<TEntity> query)
        {
            return query
                .ProjectTo<TModel>(this.Mapper.ConfigurationProvider);
        }

        public virtual IQueryable<TModel> GetQuery()
        {
            return GetQuery(QueryInternal());
        }

        public virtual async Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken)
        {
            var entity = this.Mapper.Map<TModel, TEntity>(model);
            await this.Store.AddAsync(entity, cancellationToken);
            await OnCreateAsync(entity);
            await this.UnitOfWork.SaveChangesAsync(cancellationToken);
            model.Id = entity.Id;
            return this.Mapper.Map<TModel>(entity);
        }

        protected virtual async Task OnUpdateAsync(TEntity entity)
        {
        }

        protected virtual async Task OnCreateAsync(TEntity entity)
        {
        }

        protected virtual async Task OnDeleteAsync(TEntity entity)
        {
        }

        public virtual async Task<TModel> UpdateAsync(TModel model, CancellationToken cancellationToken)
        {
            var entity = await this.Store.GetAsync(model.Id, cancellationToken);
            if (entity == null) return null;
            this.Mapper.Map(model, entity);
            await this.OnUpdateAsync(entity);
            await this.UnitOfWork.SaveChangesAsync(cancellationToken);
            return this.Mapper.Map<TModel>(entity);
        }

        public virtual async Task<TModel> DeleteAsync(TModel model, CancellationToken cancellationToken)
        {
            var entity = await this.Store.GetAsync(model.Id, cancellationToken);
            if (entity == null) return null;
            entity.IsDeleted = true;
            await this.OnDeleteAsync(entity);
            await this.UnitOfWork.SaveChangesAsync(cancellationToken);
            return this.Mapper.Map<TModel>(entity);
        }
    }
}
