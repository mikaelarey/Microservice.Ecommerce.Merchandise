namespace Merchandise.Application.Dtos.Responses.Brand
{
    public class BrandAddResponseDto
    {
        public string Status { get; set; }
        public string? ErrorMessage { get; set; }
        public BrandViewModel? Brand { get; set; }
    }

    public class BrandViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
    }


}
