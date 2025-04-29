using Merchandise.Domain.Models.Aggregates;

namespace Merchandise.Domain.DataModels.Products
{
    public class ProductQueryDataModel
    {
        public Product Product { get; set; }
        public ProductImage ProductImage { get; set; }
        public Category Category { get; set; }
        public Brand Brand { get; set; }
    }
}
