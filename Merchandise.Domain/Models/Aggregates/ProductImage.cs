using Merchandise.Domain.Models.Common;

namespace Merchandise.Domain.Models.Aggregates
{
    public class ProductImage : BaseEntity
    {
        public Guid ProductId { get; private set; }
		public bool IsPrimary { get; private set; }
		public bool IsThumbnail { get; private set; }
		public string ImageUrl { get; private set; } = string.Empty;

        public ProductImage() { }

        public ProductImage(Guid productId, bool isPrimary, bool isThumbnail, string image) : base()
        {
            ProductId = productId;
            IsPrimary = isPrimary;
            IsThumbnail = isThumbnail;
            ImageUrl = image;
        }

        public void SetProductId(Guid productId)
        {
            ProductId = productId;
        }

        public void SetAsPrimaryImage(bool isPrimaryImage)
        {
            IsPrimary = isPrimaryImage;
        }
    }
}
