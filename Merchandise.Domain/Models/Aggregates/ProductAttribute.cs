using Merchandise.Domain.Models.Common;

namespace Merchandise.Domain.Models.Aggregates
{
    public class ProductAttribute : BaseEntity
    {
        public Guid ProductId { get; private set; }
        public Guid ProductAttributeValueId { get; private set; }

        public ProductAttribute() { }
    }
}
