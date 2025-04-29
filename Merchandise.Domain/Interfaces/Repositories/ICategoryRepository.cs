using Merchandise.Domain.Models.Aggregates;

namespace Merchandise.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetCategoryByIdAsync(Guid categoryId);
    }
}
