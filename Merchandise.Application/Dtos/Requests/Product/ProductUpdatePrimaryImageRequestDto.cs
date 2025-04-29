using System.ComponentModel.DataAnnotations;

namespace Merchandise.Application.Dtos.Requests.Product
{
    public class ProductUpdatePrimaryImageRequestDto
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Guid PrimaryImageId {  get; set; }
    }
}
