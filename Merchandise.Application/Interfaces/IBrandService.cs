using Merchandise.Application.Dtos.Requests.Brand;
using Merchandise.Application.Dtos.Responses.Brand;
using Merchandise.Domain.DataModels.Brands;

namespace Merchandise.Application.Interfaces
{
    public interface IBrandService
    {
        Task<BrandAddResponseDto> AddBrandAsync(BrandAddRequestDto brand);
        Task<List<BrandDataModel>> GetActiveBrandsAsync();
        Task<BrandUpdateResponseDto> UpdateBrandAsync(BrandUpdateRequestDto brand);
        Task<BrandDeleteResponseDataModel> DeleteBrandAsync(BrandDeleteRequestDto brand);
        Task<BrandDetailResponseDto?> GetBrandAsync(Guid id);
    }
}
