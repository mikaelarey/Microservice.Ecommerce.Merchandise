namespace Merchandise.Domain.ViewModels.Products
{
    public class ProductAddViewModel
    {
        public string Name { get; set; }

        public Guid ProductId { get; set; }
        public Guid BrandId { get; set; }
        public Guid CategoryId { get; set; }

        public decimal Price { get; set; }
        public string Description { get; set; }

        public List<ProductImageViewModel> Images { get; set; }

        public string CreatedBy { get; set; } 
    }
}
