using System;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.DbSet.SingleOrDefaultAsync(predicate);
        }

        public async Task<TEntity> Get(long id)
        {
            return await this.DbSet.FindAsync(id);
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                await this.DbSet.AddAsync(entity);
            }
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
            return entity;
        }

        public async Task<TEntity> Remove(TEntity entity)
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

        public async Task<TEntity> Remove(long id)
        {
            var entity = await this.Get(id);
            return await this.Remove(entity);
        }

        public async Task Remove(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await this.DbSet.Where(predicate).ToListAsync();
            this.DbSet.RemoveRange(entities);
        }
    }
}
