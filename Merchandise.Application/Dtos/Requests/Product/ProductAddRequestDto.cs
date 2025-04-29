using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Merchandise.Application.Dtos.Requests.Product
{
    public class ProductAddRequestDto
    {
        [Required]
        [DisplayName("Product name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Brand")]
        public Guid BrandId { get; set; }

        [Required]
        [DisplayName("Category")]
        public Guid CategoryId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DisplayName("Product image/s")]
        public List<IFormFile> Images { get; set; }
    }
}
