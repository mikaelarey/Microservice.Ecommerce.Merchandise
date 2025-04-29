using Merchandise.Domain.Models.Aggregates;
using Microsoft.AspNetCore.Http;

namespace Merchandise.Domain.DataModels.Products
{
    public class ProductAddDataModel
    {
        public Product Product { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
