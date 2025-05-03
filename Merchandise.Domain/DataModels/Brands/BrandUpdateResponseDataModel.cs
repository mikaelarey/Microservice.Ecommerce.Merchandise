namespace Merchandise.Domain.DataModels.Brands
{
    public  class BrandUpdateResponseDataModel : BrandAddResponseDataModel
    {
        public string? ImageToDelete { get; set; }
    }
}
