using Microsoft.EntityFrameworkCore;

namespace Merchandise.Infrastructure.Extensions
{
    internal static class DbContextExtensions
    {
        public static async Task<T> ExecuteTransactionAsync<T>(this DbContext dbContext, Func<Task<T>> operation)
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                var result = await operation();
                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
