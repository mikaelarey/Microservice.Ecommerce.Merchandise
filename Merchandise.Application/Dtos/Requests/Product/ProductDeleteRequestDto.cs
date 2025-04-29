namespace Merchandise.Application.Dtos.Requests.Product
{
    public class ProductDeleteRequestDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset DateTimeLastUpdated { get; set; }
    }
}
