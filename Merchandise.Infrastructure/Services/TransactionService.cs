using Merchandise.Infrastructure.Persistences;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Merchandise.Infrastructure.Services
{
    public class TransactionService
    {
        public static void ApplyPendingMigrations(IServiceProvider serviceProvider)
        {
            var factory = serviceProvider.GetRequiredService<IDbContextFactory<MerchandiseDbContext>>();

            using var context = factory.CreateDbContext();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}
