namespace Merchandise.Domain.DataModels.Products
{
    public class ProductQueryFilterDataModel
    {
        public int Page { get; set; } = 1;
        public string Name { get; set; } = string.Empty;
        public Guid BrandId { get; set; }
        public Guid CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
