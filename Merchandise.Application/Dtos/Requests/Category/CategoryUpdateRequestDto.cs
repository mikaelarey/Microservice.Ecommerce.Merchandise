namespace Merchandise.Application.Dtos.Requests.Category
{
    public class CategoryUpdateRequestDto : CategoryAddRequestDto
    {
        public Guid Id { get; set; }
    }
}
