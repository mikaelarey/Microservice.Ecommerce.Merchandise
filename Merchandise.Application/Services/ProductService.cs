using Merchandise.Application.Dtos.Requests.Product;
using Merchandise.Application.Dtos.Responses.Product;
using Merchandise.Application.Interfaces;
using Merchandise.Domain.DataModels.Products;
using Merchandise.Domain.Interfaces.DomainServices;
using Merchandise.Domain.Interfaces.Queries;
using Merchandise.Domain.ViewModels.Products;

namespace Merchandise.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductDomainService _productDomainService;
        private readonly IProductQuery _productQuery;

        public ProductService(
            IHttpContextAccessor httpContextAccessor,
            IProductDomainService productDomainService,
            IProductQuery productQuery)
        {
            _httpContextAccessor = httpContextAccessor;
            _productDomainService = productDomainService;
            _productQuery = productQuery;
        }

        public async Task<AddProductResponseDto> AddProductAsync(ProductAddRequestDto product)
        {
            var response = new AddProductResponseDto();

            var productImageViewModel = product.Images
                .Select(x => new ProductImageViewModel
                {
                    FileName = Guid.NewGuid().ToString() + Path.GetExtension(x.FileName),
                    Image = x
                }).ToList();

            var productViewModel = new ProductAddViewModel
            {
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Description = product.Description,
                Images = productImageViewModel,
                Name = product.Name,
                Price = product.Price,
                CreatedBy = string.Empty
            };

            var result = await _productDomainService.AddProductAsync(productViewModel);

            if (result.ErrorMessage is not null)
            {
                response.ErrorMessage = result.ErrorMessage;
                return response;
            }
                
            await UploadImages(productImageViewModel);
            response.Id = result.Id;

            return response;
        }

        public async Task<ProductImageAddResponseDto> AddProductImagesAsync(ProductImageAddRequestDto request, string imagePath)
        {
            var productImageViewModel = request.Images
                .Select(x => new ProductImageViewModel
                {
                    FileName = Guid.NewGuid().ToString() + Path.GetExtension(x.FileName),
                    Image = x
                }).ToList();

            var result = await _productDomainService.AddProductImagesAsync(productImageViewModel, request.ProductId);

            if (result is null)
            {
                return new ProductImageAddResponseDto
                { 
                    ErrorMessage = "There was an error occured while adding images"
                };
            }

            await UploadImages(productImageViewModel);

            return new ProductImageAddResponseDto
            {
                ProductId = request.ProductId,
                Images = result.Select(x => new ProductImage
                    { 
                        Id = x.Id,
                        Url = imagePath + x.ImageUrl,
                        FileName = x.ImageUrl
                    }).ToList()
            };
        }

        public async Task<ProductImageDeleteResponseDto> DeleteProductImagesAsync(ProductImageDeleteRequestDto request)
        {
            var result = await _productDomainService.DeleteProductImagesAsync(request.ProductId, request.ImageIds);

            foreach (var item in result)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Products", item.FileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            return new ProductImageDeleteResponseDto
            {
                DeletedImageIds = result.Select(x => x.Id).ToList(),
                ProductId = request.ProductId
            };
        }

        public async Task<ProductGetDetailResponseDto?> GetProductByIdAsync(Guid id, string imagePath)
        {
            var productDetail = await _productQuery.GetProductByIdAsync(id);

            return productDetail is null
                ? null : new ProductGetDetailResponseDto()
                {
                    Id = productDetail.Product.Id,
                    BrandId = productDetail.Brand is null ? null : productDetail.Brand.Id,
                    BrandName = productDetail.Brand is null ? null : productDetail.Brand.Name,
                    CategoryId = productDetail.Category is null ? null : productDetail.Category.Id,
                    CategoryName = productDetail.Category is null ? null : productDetail.Category.Name,
                    Description = productDetail.Product.Description,
                    Name = productDetail.Product.Name,
                    Price = productDetail.Product.Price,
                    DateTimeLastUpdated = productDetail.Product.DateTimeLastUpdated,
                    Images = productDetail.Images.Select(x => new ProductImage
                        {
                            Id = x.Id,
                            Url = imagePath + x.ImageUrl,
                            FileName = x.ImageUrl
                        }).ToList(),
                    Attributes = productDetail.Attributes
                        .GroupBy(v => v.CodeAttribute.Name)
                        .Select(g => new VariantAttribute
                        {
                            Name = g.Key,
                            Values = g.Select(v => v.AttributeValue.Value).Distinct().ToList()
                        })
                        .ToList()
                }; ;
        }

        public async Task<GetProductsResponseDto> GetProductsAsync(int page, string imagePath)
        {
            var products = await _productQuery.GetProductsAsync(page);

            return new GetProductsResponseDto
            {
                Page = products.Page,
                PageSize = products.PageSize,
                TotalItems = products.TotalItems,
                Products = products.Items
                    .Select(x => new ProductResponseDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                        ImageUrl = imagePath + x.ImageUrl
                    }).ToList()
            };
        }

        public async Task<GetProductsResponseDto> SearchProductsAsync(ProductSearchRequestDto request)
        {
            var fiters = new ProductQueryFilterDataModel
            {
                Page = request.Page,
                Name = request.Name,
                CategoryId = request.CategoryId,
                BrandId = request.BrandId,
                MinPrice = request.MinPrice,
                MaxPrice = request.MaxPrice
            };

            var products = await _productQuery.GetProductsAsync(fiters);

            return new GetProductsResponseDto
            {
                Page = products.Page,
                PageSize = products.PageSize,
                TotalItems = products.TotalItems,
                Products = products.Items
                    .Select(x => new ProductResponseDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                        ImageUrl = x.ImageUrl
                    }).ToList()
            };
        }

        public async Task<bool> SetPrimaryProductImagesAsync(ProductUpdatePrimaryImageRequestDto request)
        {
            return await _productDomainService.SetProductPrimaryImageAsync(request.ProductId, request.PrimaryImageId);
        }

        public async Task<ProductUpdateResponseDto> UpdateProductDetailAsync(ProductUpdateRequestDto request)
        {
            var product = new ProductUpdateRequestDataModel
            {
                BrandId = request.BrandId,
                CategoryId = request.CategoryId,
                Description = request.Description,
                Id = request.Id,
                Name = request.Name,
                Price = request.Price,
                DateTimeLastUpdated = request.DateTimeLastUpdated
            };

            var result = await _productDomainService.UpdateProductDetailAsync(product);

            return new ProductUpdateResponseDto
            {
                ErrorMessages = result.ErrorMessages,
                Product = request,
                Status = result.Status
            };
        }

        public async Task<bool> DeleteProductAsync(ProductDeleteRequestDto product)
        {
            return await _productDomainService.DeleteProductAsync(product.Id, product.DateTimeLastUpdated);
        }

        private async Task UploadImages(List<ProductImageViewModel> images) 
        {
            var request = _httpContextAccessor.HttpContext!.Request;
            var imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Products");

            if (!Directory.Exists(imageFolder))
                Directory.CreateDirectory(imageFolder);

            foreach (var image in images)
            {
                if (image.Image.Length > 0)
                {
                    var uniqueFileName = image.FileName;
                    var filePath = Path.Combine(imageFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.Image.CopyToAsync(stream);
                    }
                }
            }
        }
    }
}
