using Merchandise.Application.Dtos.Requests.Product;
using Merchandise.Application.Dtos.Responses.Product;
using Microsoft.AspNetCore.Mvc;

namespace Merchandise.Application.Interfaces
{
    public interface IProductService
    {
        Task<AddProductResponseDto> AddProductAsync(ProductAddRequestDto product);
        Task<GetProductsResponseDto> GetProductsAsync(int page, string imagePath);
        Task<GetProductsResponseDto> SearchProductsAsync(ProductSearchRequestDto request);
        Task<ProductGetDetailResponseDto?> GetProductByIdAsync(Guid id, string imagePath);
        Task<ProductUpdateResponseDto> UpdateProductDetailAsync(ProductUpdateRequestDto request);
        Task<ProductImageAddResponseDto> AddProductImagesAsync(ProductImageAddRequestDto request, string imagePath);
        Task<ProductImageDeleteResponseDto> DeleteProductImagesAsync(ProductImageDeleteRequestDto request);
        Task<bool> SetPrimaryProductImagesAsync(ProductUpdatePrimaryImageRequestDto request);
        Task<bool> DeleteProductAsync(ProductDeleteRequestDto product);
    }
}
