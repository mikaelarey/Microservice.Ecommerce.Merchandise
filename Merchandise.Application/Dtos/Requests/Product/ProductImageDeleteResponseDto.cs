namespace Merchandise.Application.Dtos.Requests.Product
{
    public class ProductImageDeleteResponseDto
    {
        public Guid ProductId { get; set; }
        public IEnumerable<Guid>? DeletedImageIds { get; set; }
    }
}
