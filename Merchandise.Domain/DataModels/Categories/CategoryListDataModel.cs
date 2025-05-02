namespace Merchandise.Domain.DataModels.Categories
{
    public class CategoryDataModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? CategoryParentId { get; set; }
        public List<CategoryDataModel> SubCategories { get; set; } = new();
    }
}
