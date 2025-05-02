namespace Merchandise.Domain.Constants
{
    public static class CategoryMessage
    {
        public static readonly string AddErrorMessage = "An error occured while adding category";
        public static readonly string DuplcateErrorMessage = "The category name is already exists";
        public static readonly string ParentCategoryNotExistsErrorMessage = "The parent category does not exists";
        public static readonly string CategoryNotExistsErrorMessage = "The category does not exists";
        public static readonly string CategoryHasRelatedProductsErrorMessage = "There are product/s under the category";
        public static readonly string CategoryHasBeenPreviouslyUpdatedErrorMessage = "Category has been previously updated";
    }
}
