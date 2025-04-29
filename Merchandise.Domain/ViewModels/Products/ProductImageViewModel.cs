using Microsoft.AspNetCore.Http;

namespace Merchandise.Domain.ViewModels.Products
{
    public class ProductImageViewModel
    {
        public string FileName { get; set; }
        public IFormFile Image { get; set; }
    }
}
