using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sirius.Shared;
using Sirius.Shared.Entities;

namespace Sirius.Data.Access
{
    internal class Store<TEntity> : IStore<TEntity>
        where TEntity : class, IEntity
    {
        private readonly Lazy<DbSet<TEntity>> _dbSet;

        public Store(SiriusDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = new Lazy<DbSet<TEntity>>(context.Set<TEntity>);
        }

        protected SiriusDbContext Context { get; private set; }

        protected DbSet<TEntity> DbSet => _dbSet.Value;

        public IQueryable<TEntity> Query()
        {
            return this.DbSet;
        }

        public IQueryable<TEntity> Query<TProperty>(Expression<Func<TEntity, TProperty>> includeProperty)
        {
            return this.DbSet.Include(includeProperty);
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, object>>[] includeProperties)
        {
            return includeProperties
                .Aggregate((IQueryable<TEntity>)this.DbSet, (current, property) => current.Include(property));
        }

        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbSet.Where(predicate);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await this.DbSet.SingleOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await this.DbSet.FindAsync(id);
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                await this.DbSet.AddAsync(entity, cancellationToken);
            }
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
            return entity;
        }

        public async Task<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
            return entity;
        }

        public async Task<TEntity> RemoveAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await this.GetAsync(id, cancellationToken);
            return await this.RemoveAsync(entity, cancellationToken);
        }

        public async Task RemoveAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            var entities = await this.DbSet.Where(predicate).ToListAsync(cancellationToken);
            this.DbSet.RemoveRange(entities);
        }
    }
}
