namespace Merchandise.Application.Dtos.Responses.Product
{
    public class AddProductResponseDto
    {
        public List<string> ErrorMessage { get; set; } = new();
        public Guid? Id { get; set; }
    }
}
