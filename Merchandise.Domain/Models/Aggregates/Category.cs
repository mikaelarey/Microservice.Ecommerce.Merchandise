using Merchandise.Domain.Models.Common;

namespace Merchandise.Domain.Models.Aggregates
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; } = string.Empty;
        public Guid? ParentCategoryId { get; private set; }

        public Category() { }

        public Category(string name, string? description, Guid? parentCategoryId) : base()
        {
            Name = name;
            Description = description;
            ParentCategoryId = parentCategoryId;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetDescription(string? description) 
        {
            Description = description;
        }

        public void SetParentCategoryId(Guid? parentCategoryId)
        {
            ParentCategoryId = parentCategoryId;
        }
    }
}
