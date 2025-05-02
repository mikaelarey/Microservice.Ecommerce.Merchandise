using Merchandise.Application.Dtos.Requests.Category;
using Merchandise.Application.Dtos.Responses.Category;
using Merchandise.Application.Interfaces;
using Merchandise.Domain.Constants;
using Merchandise.Domain.DataModels.Categories;
using Merchandise.Domain.Interfaces.DomainServices;

namespace Merchandise.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDomainService _categoryDomainService;

        public CategoryService(ICategoryDomainService categoryDomainService)
        {
            _categoryDomainService = categoryDomainService;
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

        public async Task<CategoryDeleteResponseDataModel> DeleteCategoryAsync(Guid categoryId)
        {
            return await _categoryDomainService.DeleteCategoryAsync(categoryId);
        }

        public async Task<List<CategoryDataModel>> GetActiveCategoriesAsync()
        {
            return await _categoryDomainService.GetActiveCategoriesAsync();
        }

        public async Task<CategoryUpdateResponseDto> UpdateCategoryAsync(CategoryUpdateRequestDto category)
        {
            var result = await _categoryDomainService.UpdateCategoryAsync(category.Id, category.Name, category.Description, category.CategoryParentId);

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
