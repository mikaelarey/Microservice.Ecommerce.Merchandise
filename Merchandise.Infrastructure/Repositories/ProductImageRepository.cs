using Merchandise.Domain.Interfaces.Repositories;
using Merchandise.Domain.Models.Aggregates;
using Merchandise.Infrastructure.Persistences;
using Microsoft.EntityFrameworkCore;

namespace Merchandise.Infrastructure.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly MerchandiseDbContext _dbContext;

        public ProductImageRepository(MerchandiseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(ProductImage image)
        {
            _dbContext.ProductImage.Add(image);
        }

        public void AddRange(IEnumerable<ProductImage> images)
        {
            _dbContext.ProductImage.AddRange(images);
        }

        public void Remove(ProductImage image)
        {
            _dbContext.Remove(image);
        }

        public void RemoveRange(IEnumerable<ProductImage> images)
        {
            _dbContext.RemoveRange(images);
        }

        public void Update(ProductImage image)
        {
            _dbContext.ProductImage.Update(image);
        }

        public void UpdateRange(IEnumerable<ProductImage> images)
        {
            _dbContext.ProductImage.UpdateRange(images);
        }

        public async Task<IEnumerable<ProductImage>> GetProductImageProductById(Guid productId)
        {
            return await _dbContext.ProductImage
                .Where(x => x.ProductId == productId)
                .ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
