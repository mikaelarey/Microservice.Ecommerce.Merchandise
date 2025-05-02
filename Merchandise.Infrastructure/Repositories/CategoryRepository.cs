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

        public void Add(Category category)
        {
            _dbContext.Category.Add(category);
        }

        public void Update(Category category)
        {
            _dbContext.Category.Update(category);
        }

        public void Remove(Category category)
        {
            _dbContext.Category.Remove(category);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid categoryId, bool isTrack = false)
        {
            // await using var dbContext = _dbContextFactory.CreateDbContext();

            var query = _dbContext.Category.AsQueryable();

            if (!isTrack)
            {
                query.AsNoTracking();
            }

            return await query
                .Where(x => x.IsActive && !x.IsDeleted && !x.IsArchived)
                .FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            return await _dbContext.Category
                .AsNoTracking()
                .Where(x => x.IsActive && !x.IsDeleted && !x.IsArchived)
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<Category?> GetCategoryByIdAndNameAsync(string name, Guid id)
        {
            return await _dbContext.Category
                .AsNoTracking()
                .Where(x => x.IsActive && !x.IsDeleted && !x.IsArchived)
                .FirstOrDefaultAsync(c => c.Name == name && c.Id != id);
        }

        public async Task<IEnumerable<Category>> GetActiveCategoriesAsync()
        {
            return await _dbContext.Category
                .AsNoTracking()
                .Where(x => x.IsActive && !x.IsDeleted && !x.IsArchived)
                .ToListAsync();
        }

        
    }
}
