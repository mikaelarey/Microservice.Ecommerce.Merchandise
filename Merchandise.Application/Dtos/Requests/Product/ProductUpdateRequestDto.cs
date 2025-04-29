using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Merchandise.Application.Dtos.Requests.Product
{
    public class ProductUpdateRequestDto
    {
        [Required]
        public Guid Id { get; set; }

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
        public DateTimeOffset DateTimeLastUpdated { get; set; }
    }
}
