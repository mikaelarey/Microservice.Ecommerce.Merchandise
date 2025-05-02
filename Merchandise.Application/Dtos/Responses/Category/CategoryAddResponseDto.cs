using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Merchandise.Application.Dtos.Responses.Category
{
    public class CategoryAddResponseDto
    {
        public string Status { get; set; }
        public string? ErrorMessage { get; set; }
        public CategoryViewModel? Category { get; set; }
    }

    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid? ParentId { get; set; }
    }
}
