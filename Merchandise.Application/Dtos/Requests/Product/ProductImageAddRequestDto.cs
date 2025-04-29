using System.ComponentModel.DataAnnotations;

namespace Merchandise.Application.Dtos.Requests.Product
{
    public class ProductImageAddRequestDto
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public List<IFormFile> Images { get; set; }
    }
}
