using Merchandise.Domain.DataModels.Common;
using Merchandise.Domain.DataModels.Products;
using Merchandise.Domain.Interfaces.Queries;
using Merchandise.Infrastructure.Extensions;
using Merchandise.Infrastructure.Persistences;
using Microsoft.EntityFrameworkCore;

namespace Merchandise.Infrastructure.Queries
{
    public class ProductQuery : IProductQuery
    {
        private readonly MerchandiseDbContext _dbContext;
        private readonly int _pageSize = 20;

        public ProductQuery(MerchandiseDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<SearchResultDataModel<ProductQueryResultDataModel>> GetProductsAsync(int page = 1)
        {
            var query = BuildProductQuery();
            var result = await query.ToPaginatedResultAsync(page, _pageSize);

            return BuildProductQueryResult(result);
        }

        public async Task<SearchResultDataModel<ProductQueryResultDataModel>> GetProductsAsync(ProductQueryFilterDataModel filter)
        {
            var query = BuildProductQuery();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => x.Product.Name.Contains(filter.Name));
            }

            if (filter.CategoryId != Guid.Empty)
            {
                query = query.Where(x => x.Product.CategoryId == filter.CategoryId);
            }

            if (filter.BrandId != Guid.Empty)
            {
                query = query.Where(x => x.Product.BrandId == filter.BrandId);
            }

            if (filter.MinPrice != null && filter.MinPrice > 0)
            {
                query = query.Where(x => x.Product.Price >= filter.MinPrice);
            }

            if (filter.MaxPrice != null && filter.MaxPrice > 0)
            {
                query = query.Where(x => x.Product.Price <= filter.MaxPrice);
            }

            var result = await query.ToPaginatedResultAsync(filter.Page, _pageSize);

            return BuildProductQueryResult(result);
        }

        public async Task<ProductDetailQueryResultDataModel?> GetProductByIdAsync(Guid id)
        {
            return await (from product in _dbContext.Product
                          join category in _dbContext.Category
                             on product.CategoryId equals category.Id into categories

                          from category in categories.DefaultIfEmpty()
                          join brand in _dbContext.Brand
                             on product.BrandId equals brand.Id into brands

                          from brand in brands.DefaultIfEmpty()

                          let images = _dbContext.ProductImage
                             .Where(i => i.ProductId == product.Id)
                             .ToList()

                          let variants = (from productVariant in _dbContext.ProductVariant
                                          join variant in _dbContext.Variant
                                            on productVariant.VariantId equals variant.Id
                                          where productVariant.ProductId == product.Id
                                          select new ProductVariantDataModel
                                          {
                                              ProductVariant = productVariant,
                                              Variant = variant
                                          }).ToList()

                          let attributes = (from attributeName in _dbContext.AttributeName
                                            join attributeValue in _dbContext.AttributeValue 
                                                on attributeName.Id equals attributeValue.AttributeNameId
                                            join productAttribute in _dbContext.ProductAttribute
                                                on attributeValue.Id equals productAttribute.ProductAttributeValueId
                                            where productAttribute.ProductId == product.Id
                                            select new ProductAttributeDataModel
                                            {
                                                AttributeValue = attributeValue,
                                                ProductAttribute = productAttribute,
                                                AttributeName = attributeName
                                            }).ToList()

                          where product.Id == id

                          select new ProductDetailQueryResultDataModel
                          {
                              Product = product,
                              Category = category,
                              Brand = brand,
                              Images = images,
                              Variants = variants,
                              Attributes = attributes
                          }).FirstOrDefaultAsync();
        }

        private IQueryable<ProductQueryDataModel> BuildProductQuery() 
        { 
            return (from product in _dbContext.Product
                    join category in _dbContext.Category
                        on product.CategoryId equals category.Id into categories

                    from category in categories.DefaultIfEmpty()
                    join brand in _dbContext.Brand
                        on product.BrandId equals brand.Id into brands

                    from brand in brands.DefaultIfEmpty()

                    let image = _dbContext.ProductImage
                       .Where(i => i.ProductId == product.Id)
                       .OrderByDescending(i => i.IsPrimary)
                       .ThenBy(i => i.Id)
                       .FirstOrDefault()

                    select new ProductQueryDataModel
                    { 
                        Product = product,
                        ProductImage = image,
                        Category = category,
                        Brand = brand
                    }).AsQueryable();
        }

        private SearchResultDataModel<ProductQueryResultDataModel> BuildProductQueryResult(SearchResultDataModel<ProductQueryDataModel> result)
        {
            return new SearchResultDataModel<ProductQueryResultDataModel>
            {
                Page = result.Page,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                Items = result.Items.Select(x => new ProductQueryResultDataModel
                {
                    Brand = x.Brand is null ? "No Brand" : x.Brand.Name,
                    Category = x.Category is null ? "No Category" : x.Category.Name,
                    Id = x.Product.Id,
                    ImageUrl = x.ProductImage.ImageUrl,
                    Name = x.Product.Name,
                    Price = x.Product.Price
                }).ToList()
            };
        }
    }
}
