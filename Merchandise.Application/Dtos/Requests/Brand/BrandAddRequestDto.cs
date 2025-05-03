using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Merchandise.Application.Dtos.Requests.Brand
{
    public class BrandAddRequestDto
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image {  get; set; }
    }
}
