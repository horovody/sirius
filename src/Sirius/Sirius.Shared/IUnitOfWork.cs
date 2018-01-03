using System.Threading;
using System.Threading.Tasks;

namespace Sirius.Shared
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
    }
}
