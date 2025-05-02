using Merchandise.Application.Dtos.Requests.Category;
using Merchandise.Application.Dtos.Responses.Category;
using Merchandise.Domain.DataModels.Categories;

namespace Merchandise.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryAddResponseDto> AddCategoryAsync(CategoryAddRequestDto category);
        Task<List<CategoryDataModel>> GetActiveCategoriesAsync();
        Task<CategoryUpdateResponseDto> UpdateCategoryAsync(CategoryUpdateRequestDto category);
        Task<CategoryDeleteResponseDataModel> DeleteCategoryAsync(Guid categoryId);
    }
}
