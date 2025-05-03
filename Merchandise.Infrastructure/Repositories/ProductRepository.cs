using Merchandise.Domain.DataModels.Products;
using Merchandise.Domain.Interfaces.Repositories;
using Merchandise.Domain.Models.Aggregates;
using Merchandise.Infrastructure.Extensions;
using Merchandise.Infrastructure.Persistences;
using Microsoft.EntityFrameworkCore;

namespace Merchandise.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MerchandiseDbContext _dbContext;

        public ProductRepository(MerchandiseDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Update(Product product)
        {
            _dbContext.Product.Update(product);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<ProductAddDataModel> AddProduct(ProductAddDataModel product)
        {
            return await _dbContext.ExecuteTransactionAsync(async () =>
            {
                _dbContext.Product.Add(product.Product);
                await _dbContext.SaveChangesAsync();

                foreach (var image in product.ProductImages)
                {
                    image.SetProductId(product.Product.Id);
                    _dbContext.ProductImage.Add(image);
                }

                await _dbContext.SaveChangesAsync();

                return new ProductAddDataModel
                {
                    Product = product.Product,
                    ProductImages = product.ProductImages
                };
            });
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            return await _dbContext.Product.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product?> GetProductByNameAndCategoryAndBrandAsync(string name, Guid categoryId, Guid brandId)
        {
            return await _dbContext.Product
                .Where(x => x.Name == name && x.CategoryId == categoryId && x.BrandId == brandId)
                .FirstOrDefaultAsync();
        }

        public async Task<Product?> GetProductByNameAndCategoryAndBrandAndIdAsync(string name, Guid categoryId, Guid brandId, Guid id)
        {
            return await _dbContext.Product
                .Where(x => x.Name == name && x.CategoryId == categoryId && x.BrandId == brandId && x.Id != id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoriesAsync(IEnumerable<Guid> categoryIds)
        {
            return await _dbContext.Product
                .AsNoTracking()
                .Where(x => categoryIds.Contains(x.CategoryId) && !x.IsDeleted && x.IsActive && !x.IsArchived)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByBrandAsync(Guid brandId)
        {
            return await _dbContext.Product
                .AsNoTracking()
                .Where(x => x.IsActive && !x.IsDeleted && !x.IsArchived && x.BrandId == brandId)
                .ToListAsync();
        }
    }
}
