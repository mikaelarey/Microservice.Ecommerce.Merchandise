using Merchandise.Domain.DataModels.Common;
using Merchandise.Domain.DataModels.Products;
using Merchandise.Domain.Extensions;
using Merchandise.Domain.Interfaces.DomainServices;
using Merchandise.Domain.Interfaces.Queries;
using Merchandise.Domain.Interfaces.Repositories;
using Merchandise.Domain.Models.Aggregates;
using Merchandise.Domain.ViewModels.Products;

namespace Merchandise.Domain.DomainServices
{
    public class ProductDomainService : IProductDomainService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductQuery _productQuery;
        private readonly IBrandRepository _brandRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly DateTime _date = DateTimeExtensions.ToPhilippineTime(DateTime.UtcNow);

        public ProductDomainService(
            IProductRepository productRepository,
            IProductQuery productQuery,
            IBrandRepository brandRepository,
            ICategoryRepository categoryRepository,
            IProductImageRepository productImageRepository)
        {
            _productRepository = productRepository;
            _productQuery = productQuery;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
            _productImageRepository = productImageRepository;
        }

        #region AddProductAsync
        public async Task<ProductAddResultDataModel> AddProductAsync(AddProductViewModel productViewModel)
        {
            var errorMessages = new List<string>();
            var response = new ProductAddResultDataModel();

            var brand = await _brandRepository.GetBrandByIdAsync(productViewModel.BrandId);
            var category = await _categoryRepository.GetCategoryByIdAsync(productViewModel.CategoryId);

            if (brand == null)
            {
                errorMessages.Add("Brand not found");
            }

            if (category == null)
            {
                errorMessages.Add("Category not found");
            }

            if (errorMessages.Any() && errorMessages.Count() > 0)
            {
                response.ErrorMessage = errorMessages;
                return response;
            }


            var duplicateProduct = await _productRepository.GetProductByNameAndCategoryAndBrandAsync(productViewModel.Name, productViewModel.CategoryId, productViewModel.BrandId);

            if (duplicateProduct is not null)
            {
                errorMessages.Add("The product that you are trying to add is already exists.");
                response.ErrorMessage = errorMessages;
                return response;
            }

            var product = new Product(
                productViewModel.Name,
                productViewModel.BrandId,
                productViewModel.CategoryId,
                productViewModel.Price,
                productViewModel.Description);

            // product.SetCreatedBy(productViewModel.CreatedBy);

            var productImages = new List<ProductImage>();

            foreach (var image in productViewModel.Images)
            {
                var productImage = new ProductImage(
                    productViewModel.ProductId,
                    false,
                    false,
                    image.FileName);

                //productImage.SetCreatedBy(productViewModel.CreatedBy);
                productImages.Add(productImage);
            }

            var productDataModel = new ProductAddDataModel
            {
                Product = product,
                ProductImages = productImages
            };

            var result = await _productRepository.AddProduct(productDataModel);
            
            if (result.Product.Id == Guid.Empty)
            {
                errorMessages.Add("There's an error occured while adding the product.");
                response.ErrorMessage = errorMessages;

                return response;
            }

            response.Id = result.Product.Id;
            return response;
        }
        #endregion

        #region SearchProductsAsync
        public async Task<SearchResultDataModel<ProductQueryResultDataModel>> SearchProductsAsync(ProductQueryFilterDataModel request)
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

            return await _productQuery.GetProductsAsync(fiters);
        }
        #endregion

        #region AddProductImagesAsync
        public async Task<IEnumerable<ProductImage>?> AddProductImagesAsync(IEnumerable<ProductImageViewModel> images, Guid productId)
        {
            var productImages = new List<ProductImage>();

            foreach(var image in images)
            {
                var productImage = new ProductImage(productId, false, false, image.FileName);

                // TODO:
                //productImage.SetCreatedBy(createdBy);

                productImages.Add(productImage);
            }

            _productImageRepository.AddRange(productImages);
            var result = await _productImageRepository.SaveChangesAsync();

            return result ? productImages : null;
        }
        #endregion

        #region SetProductPrimaryImageAsync
        public async Task<bool> SetProductPrimaryImageAsync(Guid productId, Guid productImageId)
        {
            var productImages = await _productImageRepository.GetProductImageProductById(productId);

            if (productImages != null)
            {
                var productPrimaryImage = productImages.Where(x => x.Id == productImageId).FirstOrDefault();

                if (productPrimaryImage != null) 
                {
                    productPrimaryImage.SetAsPrimaryImage(true);
                    productPrimaryImage.SetDateTimeLastUpdated(_date);

                    _productImageRepository.Update(productPrimaryImage);
                }

                var productSecondaryImage = productImages.Where(x => x.Id != productImageId).ToList();

                if (productSecondaryImage != null)
                {
                    foreach (var image in productSecondaryImage)
                    {
                        image.SetAsPrimaryImage(false);
                        image.SetDateTimeLastUpdated(_date);
                    }

                    _productImageRepository.UpdateRange(productSecondaryImage);
                }
            }

            return await _productImageRepository.SaveChangesAsync();
        }
        #endregion

        #region DeleteProductImagesAsync
        public async Task<IEnumerable<Guid>> DeleteProductImagesAsync(Guid productId, IEnumerable<Guid> imageIds)
        {
            var productImages = await _productImageRepository.GetProductImageProductById(productId);

            var productImageIds = productImages
                .Where(x => imageIds.Contains(x.Id))
                .Select(x => x.Id);

            var deletedProductIds = new List<Guid>();

            foreach (var id in productImageIds)
            {
                var image = productImages
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if (image != null)
                {
                    deletedProductIds.Add(id);
                    _productImageRepository.Remove(image);
                }
            }

            if (deletedProductIds.Any())
            {
                var result = await _productImageRepository.SaveChangesAsync();
            }

            return deletedProductIds;
        }
        #endregion

        public async Task<ProductUpdateResponseDataModel> UpdateProductDetailAsync(ProductUpdateRequestDataModel product)
        {
            var errorMessages = new List<string>();

            var response = new ProductUpdateResponseDataModel
            {
                Product = product,
                Status = "Failed"
            };

            var brandTask = _brandRepository.GetBrandByIdAsync(product.BrandId);
            var categoryTask = _categoryRepository.GetCategoryByIdAsync(product.CategoryId);

            Task.WaitAll(brandTask, categoryTask);

            var brand = brandTask.Result;
            var category = categoryTask.Result;

            if (brand == null)
            {
                errorMessages.Add("Brand not found.");
            }

            if (category == null)
            {
                errorMessages.Add("Category not found.");
            }

            if (errorMessages.Any() && errorMessages.Count() > 0)
            {
                response.ErrorMessages = errorMessages;
                return response;
            }

            var productDetail = await _productRepository.GetProductByIdAsync(product.Id);

            if (productDetail is null)
            {
                errorMessages.Add("The product that you are trying to update does not exists.");
                response.ErrorMessages = errorMessages;
                return response;
            }

            var duplicateProduct = await _productRepository.GetProductByNameAndCategoryAndBrandAndIdAsync(product.Name, product.CategoryId, product.BrandId, product.Id);

            if (duplicateProduct is not null)
            {
                errorMessages.Add("Product details are duplicate record.");
                response.ErrorMessages = errorMessages;
                return response;
            }

            if (!product.DateTimeLastUpdated.Equals(productDetail.DateTimeLastUpdated))
            {
                errorMessages.Add("Product details has been already updated.");
                response.ErrorMessages = errorMessages;
                return response;
            }

            productDetail.Update(product.Name, product.BrandId, product.CategoryId, product.Price, product.Description);
            productDetail.SetDateTimeLastUpdated(_date);

            //productDetail.SetUpdatedBy(product.UpdatedBy);

            _productRepository.Update(productDetail);
            var result = await _productRepository.SaveChangesAsync();

            if (!result)
            {
                errorMessages.Add("There was an error occured while updating the product.");
                response.ErrorMessages = errorMessages;
                return response;
            }

            response.Status = "Success";
            return response;
        }

        public async Task<bool> DeleteProductAsync(Guid id, DateTimeOffset dateTimeLastUpdated)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product is not null)
            {
                if (!product.DateTimeLastUpdated.Equals(dateTimeLastUpdated) || product.IsDeleted)
                {
                    return false;
                }

                product.SetIsDeleted(true);
                product.SetIsActive(false);

                //product.SetUpdatedBy(updatedBy);
                product.SetDateTimeLastUpdated();

                _productRepository.Update(product);
                return await _productRepository.SaveChangesAsync();
            }

            return false;
        }
    }
}
