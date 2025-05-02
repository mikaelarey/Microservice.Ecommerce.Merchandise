using System.ComponentModel.DataAnnotations;

namespace Merchandise.Application.Dtos.Requests.Category
{
    public class CategoryAddRequestDto
    {
        [Required]
        public string Name { get; set; }
        public Guid? CategoryParentId { get; set; }
        public string? Description { get; set; }
    }
}
