using Merchandise.Application.Dtos.Requests.Category;
using Merchandise.Application.Dtos.Responses.Category;
using Merchandise.Application.Interfaces;
using Merchandise.Domain.Constants;
using Merchandise.Domain.DataModels.Categories;
using Merchandise.Domain.Interfaces.DomainServices;
using Merchandise.Domain.Interfaces.Repositories;

namespace Merchandise.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDomainService _categoryDomainService;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(
            ICategoryDomainService categoryDomainService,
            ICategoryRepository categoryRepository)
        {
            _categoryDomainService = categoryDomainService;
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryAddResponseDto> AddCategoryAsync(CategoryAddRequestDto category)
        {
            var result = await _categoryDomainService.AddCategoryAsync(category.Name, category.Description, category.CategoryParentId);

            return new CategoryAddResponseDto
            {
                Status = result.Status,
                ErrorMessage = result.ErrorMessage,
                Category = result.Status == RequestResponseStatus.Failed
                    ? null : new CategoryViewModel
                    {
                        Description = result.Category!.Description,
                        Id = result.Category!.Id,
                        Name = result.Category!.Name,
                        ParentId = result.Category.ParentCategoryId
                    }
            };
        }

        public async Task<CategoryDeleteResponseDataModel> DeleteCategoryAsync(CategoryDeleteRequestDto category)
        {
            return await _categoryDomainService.DeleteCategoryAsync(category.Id, category.DateTimeLastUpdate);
        }

        public async Task<List<CategoryDataModel>> GetActiveCategoriesAsync()
        {
            return await _categoryDomainService.GetActiveCategoriesAsync();
        }

        public async Task<CategoryDetailResponseDto?> GetCategoryAsync(Guid id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            return category is null
                ? null 
                : new CategoryDetailResponseDto
                {
                    CreatedBy = category.CreatedBy,
                    DateTimeCreated = category.DateTimeCreated,
                    DateTimeLastUpdated = category.DateTimeLastUpdated,
                    Description = category.Description,
                    Id = category.Id,
                    Name = category.Name,
                    ParentCategoryId = category.ParentCategoryId,
                    Status = category.IsActive ? "Active" : "Inactive",
                    UpdatedBy = category.UpdatedBy
                };
        }

        public async Task<CategoryUpdateResponseDto> UpdateCategoryAsync(CategoryUpdateRequestDto category)
        {
            var result = await _categoryDomainService.UpdateCategoryAsync(category.Id, category.Name, category.DateTimeLastUpdated, category.Description, category.CategoryParentId);

            return new CategoryUpdateResponseDto
            {
                Status = result.Status,
                ErrorMessage = result.ErrorMessage,
                Category = result.Status == RequestResponseStatus.Failed
                    ? null : new CategoryViewModel
                    {
                        Description = result.Category!.Description,
                        Id = result.Category!.Id,
                        Name = result.Category!.Name,
                        ParentId = result.Category.ParentCategoryId
                    }
            };
        }
    }
}
