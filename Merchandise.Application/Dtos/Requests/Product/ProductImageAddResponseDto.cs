namespace Merchandise.Application.Dtos.Requests.Product
{
    public class ProductImageAddResponseDto
    {
        public Guid? ProductId { get; set; }
        public List<ProductImage>? Images { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
