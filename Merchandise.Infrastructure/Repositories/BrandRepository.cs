using Merchandise.Domain.Interfaces.Repositories;
using Merchandise.Domain.Models.Aggregates;
using Merchandise.Infrastructure.Persistences;
using Microsoft.EntityFrameworkCore;

namespace Merchandise.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly MerchandiseDbContext _dbContext;
        private readonly IDbContextFactory<MerchandiseDbContext> _dbContextFactory;

        public BrandRepository(
            MerchandiseDbContext dbContext,
            IDbContextFactory<MerchandiseDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _dbContext = _dbContextFactory.CreateDbContext();
        }

        public void Add(Brand brand)
        {
            _dbContext.Add(brand);
        }

        public async Task<IEnumerable<Brand>> GetActiveBrandsAsync()
        {
            return await _dbContext.Brand
                .AsNoTracking()
                .Where(x => x.IsActive && !x.IsDeleted && !x.IsArchived)
                .ToListAsync();
        }

        public async Task<Brand?> GetBrandByIdAndNameAsync(string name, Guid id)
        {
            return await _dbContext.Brand
                .AsNoTracking()
                .Where(x => x.IsActive && !x.IsDeleted && !x.IsArchived)
                .FirstOrDefaultAsync(c => c.Name == name && c.Id != id);
        }

        public async Task<Brand?> GetBrandByIdAsync(Guid brandId, bool isTrack = false)
        {
            var query = _dbContext.Brand.AsQueryable();

            if (!isTrack)
            {
                query.AsNoTracking();
            }
              
            return await query
                .Where(x => x.IsActive && !x.IsArchived && !x.IsDeleted)
                .FirstOrDefaultAsync(b => b.Id == brandId);
        }

        public async Task<Brand?> GetBrandByNameAsync(string name)
        {
            return await _dbContext.Brand
                .AsNoTracking()
                .Where(x => x.IsActive && !x.IsDeleted && !x.IsArchived)
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        public void Remove(Brand brand)
        {
            _dbContext.Brand.Remove(brand);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public void Update(Brand brand)
        {
            _dbContext.Brand.Update(brand);

        }
    }
}
