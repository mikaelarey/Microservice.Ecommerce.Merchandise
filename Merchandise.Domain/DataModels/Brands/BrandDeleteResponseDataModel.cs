using Merchandise.Domain.DataModels.Categories;
using Merchandise.Domain.Models.Aggregates;

namespace Merchandise.Domain.DataModels.Brands
{
    public class BrandDeleteResponseDataModel
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<Product> ProductList { get; set; }
    }
}
