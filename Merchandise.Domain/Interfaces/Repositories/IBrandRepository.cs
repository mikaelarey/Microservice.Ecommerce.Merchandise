using Merchandise.Domain.Models.Aggregates;

namespace Merchandise.Domain.Interfaces.Repositories
{
    public interface IBrandRepository
    {
        Task<Brand?> GetBrandByIdAsync(Guid brandId);
    }
}
