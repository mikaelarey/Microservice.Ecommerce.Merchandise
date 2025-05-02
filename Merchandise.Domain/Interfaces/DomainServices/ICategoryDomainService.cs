using Merchandise.Domain.DataModels.Categories;
using Merchandise.Domain.Models.Aggregates;

namespace Merchandise.Domain.Interfaces.DomainServices
{
    public interface ICategoryDomainService
    {
        Task<CategoryAddResponseDataModel> AddCategoryAsync(string name, string? description, Guid? categoryParentId);
        Task<List<CategoryDataModel>> GetActiveCategoriesAsync();
        Task<CategoryUpdateResponseDataModel> UpdateCategoryAsync(Guid id, string name, string? description, Guid? categoryParentId);
        Task<List<Category>> GetAllChildCategoryIdsAsync(Guid categoryId);
        Task<CategoryDeleteResponseDataModel> DeleteCategoryAsync(Guid categoryId);
    }
}
