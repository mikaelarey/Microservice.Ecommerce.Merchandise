namespace Merchandise.Application.Dtos.Requests.Category
{
    public class CategoryDeleteRequestDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset DateTimeLastUpdate { get; set; }
    }
}
