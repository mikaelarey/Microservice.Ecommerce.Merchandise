using Merchandise.Domain.Models.Aggregates;

namespace Merchandise.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        void Add(Category category);
        void Update(Category category);
        void Remove(Category category);
        Task<bool> SaveChangesAsync();
        Task<Category?> GetCategoryByIdAsync(Guid categoryId, bool isTrack = false);
        Task<Category?> GetCategoryByNameAsync(string name);
        Task<Category?> GetCategoryByIdAndNameAsync(string name, Guid id);
        Task<IEnumerable<Category>> GetActiveCategoriesAsync();

    }
}
