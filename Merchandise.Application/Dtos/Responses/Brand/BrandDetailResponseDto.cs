namespace Merchandise.Application.Dtos.Responses.Brand
{
    public class BrandDetailResponseDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string ImageUrl { get; set; }
        public Guid Id { get; set; }
        public DateTimeOffset DateTimeCreated { get; set; }
        public DateTimeOffset DateTimeLastUpdated { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string Status { get; set; }
    }
}
