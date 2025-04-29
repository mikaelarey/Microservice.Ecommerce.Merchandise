using Merchandise.Domain.Models.Aggregates;

namespace Merchandise.Domain.Interfaces.Repositories
{
    public interface IProductImageRepository
    {
        void Add(ProductImage image);
        void AddRange(IEnumerable<ProductImage> images);
        void Update(ProductImage image);
        void UpdateRange(IEnumerable<ProductImage> images);
        void Remove(ProductImage image);
        void RemoveRange(IEnumerable<ProductImage> images);
        Task<IEnumerable<ProductImage>> GetProductImageProductById(Guid productId);
        Task<bool> SaveChangesAsync();
    }
}
