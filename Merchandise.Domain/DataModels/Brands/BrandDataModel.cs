namespace Merchandise.Domain.DataModels.Brands
{
    public class BrandDataModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string? Description { get; set; }
    }
}
