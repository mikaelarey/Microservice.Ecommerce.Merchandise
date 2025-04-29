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

        public async Task<Brand?> GetBrandByIdAsync(Guid brandId)
        {
            return await _dbContext.Brand
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == brandId);
        }
    }
}
