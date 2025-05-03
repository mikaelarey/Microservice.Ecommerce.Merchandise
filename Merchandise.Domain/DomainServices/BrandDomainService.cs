using Merchandise.Domain.Constants;
using Merchandise.Domain.DataModels.Brands;
using Merchandise.Domain.Interfaces.DomainServices;
using Merchandise.Domain.Interfaces.Repositories;
using Merchandise.Domain.Models.Aggregates;

namespace Merchandise.Domain.DomainServices
{
    public class BrandDomainService : IBrandDomainService
    {
        public readonly IBrandRepository _brandRepository;
        public readonly IProductRepository _productRepository;

        public BrandDomainService(
            IBrandRepository brandRepository,
            IProductRepository productRepository)
        {
            _brandRepository = brandRepository;
            _productRepository = productRepository;
        }

        public async Task<BrandAddResponseDataModel> AddBrandAsync(string name, string imageFileName, string? description)
        {
            var response = new BrandAddResponseDataModel();
            var existingBrand = await _brandRepository.GetBrandByNameAsync(name);

            if (existingBrand is not null)
            {
                response.Status = RequestResponseStatus.Failed;
                response.ErrorMessage = BrandMessage.DuplcateErrorMessage;
                return response;
            }

            var brand = new Brand(name, imageFileName, description);
            // category.SetCreatedBy();

            _brandRepository.Add(brand);
            var result = await _brandRepository.SaveChangesAsync();

            response.Status = result ? RequestResponseStatus.Success : RequestResponseStatus.Failed;
            response.ErrorMessage = result ? null : BrandMessage.AddErrorMessage;
            response.Brand = result ? brand : null;

            return response;
        }

        public async Task<BrandDeleteResponseDataModel> DeleteBrandAsync(Guid BrandId, DateTimeOffset dateTimeLastUpdated)
        {
            var response = new BrandDeleteResponseDataModel();
            var brand = await _brandRepository.GetBrandByIdAsync(BrandId, true);

            if (brand is null)
            {
                response.Status = RequestResponseStatus.Failed;
                response.ErrorMessage = BrandMessage.BrandNotExistsErrorMessage;

                return response;
            }

            var products = await _productRepository.GetProductsByBrandAsync(BrandId);

            if (products is not null && products.Any())
            {
                response.Status = RequestResponseStatus.Failed;
                response.ErrorMessage = BrandMessage.BrandHasRelatedProductsErrorMessage;
                response.ProductList = products;

                return response;
            }

            if (!brand.DateTimeLastUpdated.Equals(dateTimeLastUpdated))
            {
                response.Status = RequestResponseStatus.Failed;
                response.ErrorMessage = BrandMessage.BrandHasBeenPreviouslyUpdatedErrorMessage;

                return response;
            }

            brand.SetIsActive(false);
            brand.SetIsDeleted(true);
            brand.SetDateTimeLastUpdated();
            // category.SetUpdatedBy();

            var result = await _brandRepository.SaveChangesAsync();

            response.Status = RequestResponseStatus.Success;
            return response;
        }

        public async Task<List<BrandDataModel>> GetActiveBrandsAsync()
        {
            var result = await _brandRepository.GetActiveBrandsAsync();

            return result.Select(b => new BrandDataModel() 
            {
                Description = b.Description,
                Id = b.Id,
                LogoUrl = b.LogoUrl,
                Name = b.Name,
            }).ToList();
        }

        public Task<List<Brand>> GetAllChildBrandIdsAsync(Guid BrandId)
        {
            throw new NotImplementedException();
        }

        public async Task<BrandUpdateResponseDataModel> UpdateBrandAsync(Guid id, string name, string? imageFileName, DateTimeOffset dateTimeLastUpdated, string? description)
        {
            var response = new BrandUpdateResponseDataModel();
            var brand = await _brandRepository.GetBrandByIdAsync(id, true);

            if (brand is null)
            {
                response.Status = RequestResponseStatus.Failed;
                response.ErrorMessage = BrandMessage.BrandNotExistsErrorMessage;

                return response;
            }

            var duplicateCategory = await _brandRepository.GetBrandByIdAndNameAsync(name, id);

            if (duplicateCategory is not null)
            {
                response.Status = RequestResponseStatus.Failed;
                response.ErrorMessage = BrandMessage.DuplcateErrorMessage;

                return response;
            }

            if (!brand.DateTimeLastUpdated.Equals(dateTimeLastUpdated))
            {
                response.Status = RequestResponseStatus.Failed;
                response.ErrorMessage = BrandMessage.BrandHasBeenPreviouslyUpdatedErrorMessage;

                return response;
            }

            var fileName = imageFileName ?? brand.LogoUrl;
            var imageToDelete = imageFileName is not null ? imageFileName : null;

            brand.SetName(name);
            brand.SetDescription(description);
            brand.SetLogoUrl(fileName);
            brand.SetDateTimeLastUpdated();
            // category.SetUpdatedBy();

            _brandRepository.Update(brand);
            var result = await _brandRepository.SaveChangesAsync();

            response.Status = result ? RequestResponseStatus.Success : RequestResponseStatus.Failed;
            response.ErrorMessage = result ? null : BrandMessage.AddErrorMessage;
            response.ImageToDelete = imageToDelete;
            response.Brand = result ? brand : null;

            return response;
        }
    }
}
