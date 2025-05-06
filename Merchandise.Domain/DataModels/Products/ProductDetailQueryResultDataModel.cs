using Merchandise.Domain.Models.Aggregates;

namespace Merchandise.Domain.DataModels.Products
{
    public class ProductDetailQueryResultDataModel
    {
        public Product Product { get; set; } 
        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public List<ProductImage> Images { get; set; }
        public List<ProductAttributeDataModel> Attributes { get; set; }
    }

   

    public class ProductAttributeDataModel
    {
        public ProductAttribute ProductAttribute { get; set; }
        public CodeDecodeAttribute CodeAttribute { get; set; }
        public AttributeValue AttributeValue { get; set; }
    }
}
