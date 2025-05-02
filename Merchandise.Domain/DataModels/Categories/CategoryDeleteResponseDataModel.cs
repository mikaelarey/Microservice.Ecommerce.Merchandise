using Merchandise.Domain.Models.Aggregates;

namespace Merchandise.Domain.DataModels.Categories
{
    public class CategoryDeleteResponseDataModel
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<CategoryProductsDataModel> ProductList { get; set; }
    }

    public class CategoryProductsDataModel
    {
        public Category Category { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
