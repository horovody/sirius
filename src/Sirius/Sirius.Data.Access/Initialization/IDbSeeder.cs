using System.Threading.Tasks;

namespace Sirius.Data.Access.Initialization
{
    /// <summary>
    /// Db seeder
    /// </summary>
    public interface IDbSeeder
    {
        Task SeedAsync(SiriusDbContext context);
    }
}
