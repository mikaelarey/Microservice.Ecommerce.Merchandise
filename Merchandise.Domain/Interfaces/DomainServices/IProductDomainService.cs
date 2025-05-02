using Merchandise.Domain.DataModels.Common;
using Merchandise.Domain.DataModels.Products;
using Merchandise.Domain.Models.Aggregates;
using Merchandise.Domain.ViewModels.Products;

namespace Merchandise.Domain.Interfaces.DomainServices
{
    public interface IProductDomainService
    {
        Task<ProductAddResultDataModel> AddProductAsync(ProductAddViewModel productViewModel);
        Task<SearchResultDataModel<ProductQueryResultDataModel>> SearchProductsAsync(ProductQueryFilterDataModel request);
        Task<IEnumerable<ProductImage>?> AddProductImagesAsync(IEnumerable<ProductImageViewModel> images, Guid productId);
        Task<IEnumerable<(Guid Id, string FileName)>> DeleteProductImagesAsync(Guid productId, IEnumerable<Guid> imageIds);
        Task<bool> SetProductPrimaryImageAsync(Guid productId, Guid productImageId);
        Task<ProductUpdateResponseDataModel> UpdateProductDetailAsync(ProductUpdateRequestDataModel product);
        Task<bool> DeleteProductAsync(Guid id, DateTimeOffset dateTimeLastUpdated);
    }
}
