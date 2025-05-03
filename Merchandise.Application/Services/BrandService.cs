using Merchandise.Application.Dtos.Requests.Brand;
using Merchandise.Application.Dtos.Responses.Brand;
using Merchandise.Application.Dtos.Responses.Category;
using Merchandise.Application.Interfaces;
using Merchandise.Domain.Constants;
using Merchandise.Domain.DataModels.Brands;
using Merchandise.Domain.DomainServices;
using Merchandise.Domain.Interfaces.DomainServices;
using Merchandise.Domain.Interfaces.Repositories;
using Merchandise.Domain.Models.Aggregates;

namespace Merchandise.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBrandDomainService _brandDomainService;
        private readonly IBrandRepository _brandRepository;

        public BrandService(
            IBrandDomainService brandDomainService, 
            IHttpContextAccessor httpContextAccessor,
            IBrandRepository brandRepository)
        {
            _brandDomainService = brandDomainService;
            _httpContextAccessor = httpContextAccessor;
            _brandRepository = brandRepository;
        }
        public async Task<BrandAddResponseDto> AddBrandAsync(BrandAddRequestDto brand)
        {
            var imageFileName = Guid.NewGuid().ToString() + Path.GetExtension(brand.Image.FileName);
            var result = await _brandDomainService.AddBrandAsync(brand.Name, imageFileName, brand.Description);
            await UploadImages(brand.Image, imageFileName);

            return new BrandAddResponseDto
            {
                Status = result.Status,
                ErrorMessage = result.ErrorMessage,
                Brand = result.Status == RequestResponseStatus.Failed
                    ? null : new BrandViewModel
                    {
                        Id = result.Brand!.Id,
                        Name = result.Brand!.Name,
                        FileName = imageFileName,
                    }
            };
        }

        public async Task<BrandDeleteResponseDataModel> DeleteBrandAsync(BrandDeleteRequestDto brand)
        {
            return await _brandDomainService.DeleteBrandAsync(brand.Id, brand.DateTimeLastUpdate);
        }

        public async Task<List<BrandDataModel>> GetActiveBrandsAsync()
        {
            return await _brandDomainService.GetActiveBrandsAsync();
        }

        public async Task<BrandUpdateResponseDto> UpdateBrandAsync(BrandUpdateRequestDto brand)
        {
            var imageFileName = brand.Image is null
                ? null : Guid.NewGuid().ToString() + Path.GetExtension(brand.Image.FileName);

            var result = await _brandDomainService.UpdateBrandAsync(brand.Id, brand.Name, imageFileName, brand.DateTimeLastUpdated, brand.Description);
            
            if (result.Status == RequestResponseStatus.Success)
            {
                if (imageFileName is not null && brand.Image is not null)
                {
                    await UploadImages(brand.Image, imageFileName);

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Brands", result.ImageToDelete!);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
            }
            
            return new BrandUpdateResponseDto
            {
                Status = result.Status,
                ErrorMessage = result.ErrorMessage,
                Brand = result.Status == RequestResponseStatus.Failed
                ? null : new BrandViewModel
                {
                    Id = result.Brand!.Id,
                    Name = result.Brand!.Name,
                    FileName = imageFileName ?? result.Brand.LogoUrl
                }
            };
        }

        private async Task UploadImages(IFormFile image, string filename)
        {
            var request = _httpContextAccessor.HttpContext!.Request;
            var imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Brands");

            if (!Directory.Exists(imageFolder))
            {
                Directory.CreateDirectory(imageFolder);
            }
                
            if (image.Length > 0)
            {
                var filePath = Path.Combine(imageFolder, filename);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
            }

        }

        public async Task<BrandDetailResponseDto?> GetBrandAsync(Guid id)
        {
            var brand = await _brandRepository.GetBrandByIdAsync(id);

            return brand is null
                ? null
                : new BrandDetailResponseDto
                {
                    CreatedBy = brand.CreatedBy,
                    DateTimeCreated = brand.DateTimeCreated,
                    DateTimeLastUpdated = brand.DateTimeLastUpdated,
                    Description = brand.Description,
                    Id = brand.Id,
                    Name = brand.Name,
                    Status = brand.IsActive ? "Active" : "Inactive",
                    UpdatedBy = brand.UpdatedBy,
                    ImageUrl = brand.LogoUrl
                };
        }
    }
}
