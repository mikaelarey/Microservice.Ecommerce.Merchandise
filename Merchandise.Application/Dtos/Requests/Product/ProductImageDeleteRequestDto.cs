namespace Merchandise.Application.Dtos.Requests.Product
{
    public class ProductImageDeleteRequestDto
    {
        public Guid ProductId { get; set; }
        public List<Guid> ImageIds { get; set; }
    }
}
