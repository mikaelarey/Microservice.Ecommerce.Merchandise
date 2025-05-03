namespace Merchandise.Application.Dtos.Requests.Brand
{
    public class BrandUpdateRequestDto : BrandAddRequestDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset DateTimeLastUpdated { get; set; }
    }
}
