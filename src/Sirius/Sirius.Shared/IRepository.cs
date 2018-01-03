using System.Linq;
using System.Threading.Tasks;
using Sirius.Shared.Entities;

namespace Sirius.Shared
{
    public interface IRepository
    {
        IStore<TEntity> Store<TEntity>() where TEntity : class, IEntity;
        Task<TEntity> Get<TEntity>(long id) where TEntity : class, IEntity;
        Task<TEntity> GetExists<TEntity>(long id) where TEntity : class, IEntity;
        IQueryable<TEntity> Query<TEntity>() where TEntity : class, IEntity;
        IQueryable<TEntity> Exists<TEntity>() where TEntity : class, IEntity;
        Task<TEntity> Add<TEntity>(TEntity entity) where TEntity : class, IEntity;
        Task<TEntity> Update<TEntity>(TEntity entity) where TEntity : class, IEntity;
        Task<TEntity> Delete<TEntity>(TEntity entity) where TEntity : class, IEntity;
    }
}
