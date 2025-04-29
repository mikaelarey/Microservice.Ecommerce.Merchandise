namespace Merchandise.Domain.DataModels.Products
{
    public class ProductUpdateResponseDataModel
    {
        public string Status { get; set; } = string.Empty;
        public List<string>? ErrorMessages { get; set; }
        public ProductUpdateRequestDataModel Product { get; set; }
    }
}
