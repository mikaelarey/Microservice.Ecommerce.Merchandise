using Merchandise.Domain.DataModels.Products;
using Merchandise.Domain.Models.Aggregates;

namespace Merchandise.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        void Update(Product product);
        Task<bool> SaveChangesAsync();
        Task<ProductAddDataModel> AddProduct(ProductAddDataModel product);
        Task<Product?> GetProductByIdAsync(Guid id);
        Task<Product?> GetProductByNameAndCategoryAndBrandAsync(string name, Guid categoryId, Guid brandId);
        Task<Product?> GetProductByNameAndCategoryAndBrandAndIdAsync(string name, Guid categoryId, Guid brandId, Guid id);
        Task<IEnumerable<Product>> GetProductsByCategoriesAsync(IEnumerable<Guid> categoryIds);
    }
}
