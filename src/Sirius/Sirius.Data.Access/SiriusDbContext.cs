using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sirius.Data.Access
{
    public class SiriusDbContext: IdentityDbContext
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="options"></param>
        public SiriusDbContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
