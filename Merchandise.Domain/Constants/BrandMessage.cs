namespace Merchandise.Domain.Constants
{
    public static class BrandMessage
    {
        public static readonly string AddErrorMessage = "An error occured while adding brand";
        public static readonly string DuplcateErrorMessage = "The Brand name is already exists";
        public static readonly string ParentBrandNotExistsErrorMessage = "The parent Brand does not exists";
        public static readonly string BrandNotExistsErrorMessage = "The Brand does not exists";
        public static readonly string BrandHasRelatedProductsErrorMessage = "There are product/s under the brand";
        public static readonly string BrandHasBeenPreviouslyUpdatedErrorMessage = "Brand has been previously updated";
    }
}
