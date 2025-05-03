using Merchandise.Domain.Models.Aggregates;

namespace Merchandise.Domain.Interfaces.Repositories
{
    public interface IBrandRepository
    {
        void Add(Brand brand);
        void Update(Brand brand);
        void Remove(Brand brand);
        Task<bool> SaveChangesAsync();
        Task<Brand?> GetBrandByIdAsync(Guid brandId, bool isTrack = false);
        Task<Brand?> GetBrandByNameAsync(string name);
        Task<Brand?> GetBrandByIdAndNameAsync(string name, Guid id);
        Task<IEnumerable<Brand>> GetActiveBrandsAsync();
    }
}
