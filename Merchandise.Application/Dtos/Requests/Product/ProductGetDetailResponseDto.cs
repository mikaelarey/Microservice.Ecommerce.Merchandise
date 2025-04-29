using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Merchandise.Application.Dtos.Requests.Product
{
    public class ProductGetDetailResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? BrandId { get; set; }
        public string? BrandName { get; set; }
        public Guid? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<ProductImage> Images { get; set; }
        public List<string> Variants { get; set; }
        public List<VariantAttribute> Attributes { get; set; }
        public DateTimeOffset DateTimeLastUpdated { get; set; }

    }

    public class VariantAttribute
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Values { get; set; } = new List<string>();
    }

    public class ProductImage
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
    }
}
