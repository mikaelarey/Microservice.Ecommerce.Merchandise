using Merchandise.Domain.Models.Common;

namespace Merchandise.Domain.Models.Aggregates
{
    public class ProductVariant : BaseEntity
    {
        public Guid ProductId { get; private set; }
        public Guid VariantId { get; private set; }

        public ProductVariant() { }
    }
}
