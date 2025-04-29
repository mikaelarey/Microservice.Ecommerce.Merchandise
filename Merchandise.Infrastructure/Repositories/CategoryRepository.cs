using Merchandise.Domain.Interfaces.Repositories;
using Merchandise.Domain.Models.Aggregates;
using Merchandise.Infrastructure.Persistences;
using Microsoft.EntityFrameworkCore;

namespace Merchandise.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MerchandiseDbContext _dbContext;
        private readonly IDbContextFactory<MerchandiseDbContext> _dbContextFactory;
        public CategoryRepository(
            MerchandiseDbContext dbContext,
            IDbContextFactory<MerchandiseDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _dbContext = _dbContextFactory.CreateDbContext();
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid categoryId)
        {
            // await using var dbContext = _dbContextFactory.CreateDbContext();

            return await _dbContext.Category
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == categoryId);
        }
    }
}
