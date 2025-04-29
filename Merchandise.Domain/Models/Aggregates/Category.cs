using Merchandise.Domain.Models.Common;

namespace Merchandise.Domain.Models.Aggregates
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string ParentCategory { get; private set; } = string.Empty;
        public bool IsRootCategory { get; private set; }

        public Category() { }

        public Category(string name, string description, string parentCategory, bool isRootCategory) : base()
        {
            Name = name;
            Description = description;
            ParentCategory = parentCategory;
            IsRootCategory = isRootCategory;
        }
    }
}
