using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Merchandise.Infrastructure.Persistences
{
    public class MerchandiseDbContextFactory : IDesignTimeDbContextFactory<MerchandiseDbContext>
    {
        public MerchandiseDbContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(basePath, "appsettings.json");


            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException($"Configuration file not found: {configPath}");
            }

            //// Load configuration
            var configuration = new ConfigurationBuilder()
                 .AddJsonFile(configPath, optional: false, reloadOnChange: true)
                 .Build();

            var connectionString = configuration.GetConnectionString("MerchandiseDb");

            var optionsBuilder = new DbContextOptionsBuilder<MerchandiseDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new MerchandiseDbContext(optionsBuilder.Options);
        }

    }
}
