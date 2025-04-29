using Merchandise.Domain.DataModels.Common;
using Merchandise.Domain.DataModels.Products;
using Merchandise.Domain.Models.Aggregates;

namespace Merchandise.Domain.Interfaces.Queries
{
    public interface IProductQuery
    {
        Task<SearchResultDataModel<ProductQueryResultDataModel>> GetProductsAsync(int page = 1);
        Task<SearchResultDataModel<ProductQueryResultDataModel>> GetProductsAsync(ProductQueryFilterDataModel filter);
        Task<ProductDetailQueryResultDataModel?> GetProductByIdAsync(Guid id);
    }
}
