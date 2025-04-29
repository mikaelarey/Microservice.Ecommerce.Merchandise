using Merchandise.Domain.Models.Common;

namespace Merchandise.Domain.Models.Aggregates
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public Guid BrandId { get; private set; }
        public Guid CategoryId { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; } = string.Empty;

        public Product() { }

        public Product(string name, Guid brandId, Guid categoryId, decimal price, string description) : base()
        {
            Name = name;
            BrandId = brandId;
            CategoryId = categoryId;
            Price = price;
            Description = description;
        }

        public void Update(string name, Guid brandId, Guid categoryId, decimal price, string description)
        {
            Name = name;
            BrandId = brandId;
            CategoryId = categoryId;
            Price = price;
            Description = description;
            SetDateTimeLastUpdated();
        }
    }
}
