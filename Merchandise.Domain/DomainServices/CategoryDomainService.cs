using Merchandise.Domain.Constants;
using Merchandise.Domain.DataModels.Categories;
using Merchandise.Domain.Interfaces.DomainServices;
using Merchandise.Domain.Interfaces.Repositories;
using Merchandise.Domain.Models.Aggregates;

namespace Merchandise.Domain.DomainServices
{
    public class CategoryDomainService : ICategoryDomainService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public CategoryDomainService(
            ICategoryRepository categoryRepository,
            IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<CategoryAddResponseDataModel> AddCategoryAsync(string name, string? description, Guid? parentCategoryId)
        {
            var response = new CategoryAddResponseDataModel();
            var existingCategory = await _categoryRepository.GetCategoryByNameAsync(name);

            if (existingCategory is not null)
            {
                response.Status = RequestResponseStatus.Failed;
                response.ErrorMessage = CategoryMessage.DuplcateErrorMessage;

                return response;
            }

            if (parentCategoryId is not null && parentCategoryId != Guid.Empty)
            {
                var parentCategory = await _categoryRepository.GetCategoryByIdAsync((Guid)parentCategoryId);

                if (parentCategory is null) 
                {
                    response.Status = RequestResponseStatus.Failed;
                    response.ErrorMessage = CategoryMessage.ParentCategoryNotExistsErrorMessage;

                    return response;
                }
            }

            var category = new Category(name, description, parentCategoryId);
            // category.SetCreatedBy();

            _categoryRepository.Add(category);
            var result = await _categoryRepository.SaveChangesAsync();

            response.Status = result ? RequestResponseStatus.Success : RequestResponseStatus.Failed;
            response.ErrorMessage = CategoryMessage.AddErrorMessage;
            response.Category = result ? category : null;

            return response;
        }

        public async Task<List<CategoryDataModel>> GetActiveCategoriesAsync()
        {
            var categories = await _categoryRepository.GetActiveCategoriesAsync();
            return BuildTree(categories);
        }

        public async Task<CategoryUpdateResponseDataModel> UpdateCategoryAsync(Guid id, string name, DateTimeOffset dateTimeLastUpdated, string? description, Guid? categoryParentId)
        {
            var response = new CategoryUpdateResponseDataModel();
            var category = await _categoryRepository.GetCategoryByIdAsync(id, true);

            if (category is null)
            {
                response.Status = RequestResponseStatus.Failed;
                response.ErrorMessage = CategoryMessage.CategoryNotExistsErrorMessage;

                return response;
            }

            var duplicateCategory = await _categoryRepository.GetCategoryByIdAndNameAsync(name, id);

            if (duplicateCategory is not null)
            {
                response.Status = RequestResponseStatus.Failed;
                response.ErrorMessage = CategoryMessage.DuplcateErrorMessage;

                return response;
            }

            if (categoryParentId is not null && categoryParentId != Guid.Empty)
            {
                var parentCategory = await _categoryRepository.GetCategoryByIdAsync((Guid)categoryParentId);

                if (parentCategory is null)
                {
                    response.Status = RequestResponseStatus.Failed;
                    response.ErrorMessage = CategoryMessage.ParentCategoryNotExistsErrorMessage;

                    return response;
                }
            }

            // TODO: Check concurrency
            if (!category.DateTimeLastUpdated.Equals(dateTimeLastUpdated))
            {
                response.Status = RequestResponseStatus.Failed;
                response.ErrorMessage = CategoryMessage.CategoryHasBeenPreviouslyUpdatedErrorMessage;

                return response;
            }

            category.SetName(name);
            category.SetDescription(description);
            category.SetParentCategoryId(categoryParentId);
            category.SetDateTimeLastUpdated();
            // category.SetUpdatedBy();

            _categoryRepository.Update(category);
            var result = await _categoryRepository.SaveChangesAsync();

            response.Status = result ? RequestResponseStatus.Success : RequestResponseStatus.Failed;
            response.ErrorMessage = CategoryMessage.AddErrorMessage;
            response.Category = result ? category : null;

            return response;
        }

        public async Task<CategoryDeleteResponseDataModel> DeleteCategoryAsync(Guid categoryId, DateTimeOffset dateTimeLastUpdated)
        {
            var response = new CategoryDeleteResponseDataModel();
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId, true);

            if (category is null)
            {
                response.Status = RequestResponseStatus.Failed;
                response.ErrorMessage = CategoryMessage.CategoryNotExistsErrorMessage;

                return response;
            }

            var categories = await GetAllChildCategoryIdsAsync(categoryId);
            var categoryIds = categories.Select(c => c.Id).ToList();

            var products = await _productRepository.GetProductsByCategoriesAsync(categoryIds.Distinct());

            if (products is not null && products.Any())
            {
                var categoryProductsList = categories
                    .Select(c => new CategoryProductsDataModel
                    {
                        Category = c,
                        Products = products.Where(p => p.CategoryId == c.Id).ToList()
                    })
                    .ToList();

                categoryProductsList = categoryProductsList.Where(x => x.Products.Any()).ToList();

                response.Status = RequestResponseStatus.Failed;
                response.ErrorMessage = CategoryMessage.CategoryHasRelatedProductsErrorMessage;
                response.ProductList = categoryProductsList;

                return response;
            }

            if (!category.DateTimeLastUpdated.Equals(dateTimeLastUpdated))
            {
                response.Status = RequestResponseStatus.Failed;
                response.ErrorMessage = CategoryMessage.CategoryHasBeenPreviouslyUpdatedErrorMessage;

                return response;
            }

            category.SetIsActive(false);
            category.SetIsDeleted(true);
            category.SetDateTimeLastUpdated();
            // category.SetUpdatedBy();

            var result = await _categoryRepository.SaveChangesAsync();

            response.Status = RequestResponseStatus.Success;
            return response;
        }

        public async Task<List<Category>> GetAllChildCategoryIdsAsync(Guid categoryId)
        {
            var categories = await _categoryRepository.GetActiveCategoriesAsync();
            var result = GetDescendantIds(categoryId, categories);

            var parentCategory = categories.Where(x => x.Id == categoryId).FirstOrDefault();

            result.Add(parentCategory!);

            return result;
        }

        private static List<Category> GetDescendantIds(Guid parentId, IEnumerable<Category> categories)
        {
            var result = new List<Category>();
            var directChildren = categories.Where(c => c.ParentCategoryId == parentId).ToList();

            foreach (var child in directChildren)
            {
                result.Add(child);
                result.AddRange(GetDescendantIds(child.Id, categories)); // recursive
            }

            return result;
        }

        private static List<CategoryDataModel> BuildTree(IEnumerable<Category> categories, Guid? parentId = null)
        {
            return categories
                .Where(c => c.ParentCategoryId == parentId)
                .Select(c => new CategoryDataModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    CategoryParentId = c.ParentCategoryId,
                    SubCategories = BuildTree(categories, c.Id)
                })
               .ToList();
        }
    }
}
