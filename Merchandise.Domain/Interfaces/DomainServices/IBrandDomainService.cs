using Merchandise.Domain.DataModels.Brands;
using Merchandise.Domain.Models.Aggregates;

namespace Merchandise.Domain.Interfaces.DomainServices
{
    public interface IBrandDomainService
    {
        Task<BrandAddResponseDataModel> AddBrandAsync(string name, string imageFileName, string? description);
        Task<List<BrandDataModel>> GetActiveBrandsAsync();
        Task<BrandUpdateResponseDataModel> UpdateBrandAsync(Guid id, string name, string? imageFileName, DateTimeOffset dateTimeLastUpdated, string? description);
        Task<List<Brand>> GetAllChildBrandIdsAsync(Guid BrandId);
        Task<BrandDeleteResponseDataModel> DeleteBrandAsync(Guid BrandId, DateTimeOffset dateTimeLastUpdated);
    }
}
