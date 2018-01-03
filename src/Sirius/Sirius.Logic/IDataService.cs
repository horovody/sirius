using System.Linq;
using System.Threading.Tasks;
using Sirius.Shared.Entities;

namespace Sirius.Logic
{
    public interface IReadDataService<TModel>
        where TModel : class, IEntityTransferObject
    {
        Task<TModel> GetAsync(long id);
    }

    public interface IQueryDataService<out TModel>
        where TModel : class, IEntityTransferObject
    {
        IQueryable<TModel> GetQuery();
    }

    public interface ICreateDataService<TModel>
        where TModel : class, IEntityTransferObject
    {
        Task<TModel> CreateAsync(TModel model);
    }

    public interface IUpdateDataService<TModel>
        where TModel : class, IEntityTransferObject
    {
        Task<TModel> UpdateAsync(TModel update);
    }

    public interface IDeleteDataService<TModel>
        where TModel : class, IEntityTransferObject
    {
        Task<TModel> DeleteAsync(TModel model);
    }

    public interface ICrudDataService<TEntity, TModel> :
        IReadDataService<TModel>,
        IQueryDataService<TModel>,
        ICreateDataService<TModel>,
        IUpdateDataService<TModel>,
        IDeleteDataService<TModel> where TEntity : class, IEntity
        where TModel : class, IEntityTransferObject
    {
    }
}
