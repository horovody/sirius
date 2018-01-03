using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Sirius.Data.Access;

namespace Sirius.Modules
{
    /// <summary>
    /// Used For EF Core Migrations
    /// </summary>
    public class DesignTimeDbContextFactory: IDesignTimeDbContextFactory<SiriusDbContext>
    {
        public SiriusDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<SiriusDbContext>();
            var connectionString = configuration.GetConnectionString("SiriusConnection");
            builder.UseNpgsql(connectionString);
            return new SiriusDbContext(builder.Options, configuration);
        }
    }
}
