using Merchandise.Application.Dtos.Requests.Product;
using Merchandise.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Merchandise.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #region GET Get Products
        [HttpGet("Get")]
        public async Task<IActionResult> GetProducts(int pageNo)
        {
            var imagePath = GetImagePath();
            var result = await _productService.GetProductsAsync(pageNo, imagePath);
            return Ok(result);
        }
        #endregion

        #region GET Get Product By Id
        [HttpGet("Detail/{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var imagePath = GetImagePath();
            var result = await _productService.GetProductByIdAsync(id, imagePath);
            return (result == null) ? NotFound() : Ok(result);
        }
        #endregion

        #region POST Add Product
        [HttpPost("Add")]
        public async Task<IActionResult> AddProduct(ProductAddRequestDto product)
        {
            // TODO: Check roles
            // TODO: Fix CreatedBy
            var createdBy = User.Identity?.Name ?? string.Empty;
            var result = await _productService.AddProductAsync(product);

            return result.ErrorMessage.Any()
                ? BadRequest(new { Errors = result.ErrorMessage })
                : Created(string.Empty, new { Id = result.Id });
        }
        #endregion

        #region POST Add Product Images
        [HttpPost("AddImages")]
        public async Task<IActionResult> AddProductImages(ProductImageAddRequestDto request)
        {
            // TODO: Check roles
            // TODO: Fix CreatedBy
            var imagePath = GetImagePath();
            var result = await _productService.AddProductImagesAsync(request, imagePath);
            return Ok(result);
        }
        #endregion

        #region POST Search Products
        [HttpPost("Search")]
        public async Task<IActionResult> SearchProducts(ProductSearchRequestDto request)
        {
            var result = await _productService.SearchProductsAsync(request);
            return Ok(result);
        }
        #endregion

        #region PUT Update Product Detail
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProductDetail(ProductUpdateRequestDto request)
        {
            // TODO: Check roles
            // TODO: Fix CreatedBy
            var result = await _productService.UpdateProductDetailAsync(request);
            return result.ErrorMessages is not null ? BadRequest(result) : Ok(result);
        }
        #endregion

        #region PUT Set Primary Product Images
        [HttpPut("SetPrimaryImage")]
        public async Task<IActionResult> SetPrimaryProductImages(ProductUpdatePrimaryImageRequestDto request)
        {
            // TODO: Check roles
            // TODO: Fix CreatedBy
            var result = await _productService.SetPrimaryProductImagesAsync(request);
            return !result ? BadRequest(new { Errors = new[] { "Failed to set primary image." } }) : Ok();
        }
        #endregion

        #region DELETE Delete Product Images
        [HttpDelete("DeleteImages")]
        public async Task<IActionResult> DeleteProductImages(ProductImageDeleteRequestDto request)
        {
            // TODO: Check roles
            // TODO: Fix CreatedBy
            var result = await _productService.DeleteProductImagesAsync(request);

            return result.DeletedImageIds is not null && result.DeletedImageIds.Any()
                ? Ok(result) : BadRequest(new { Errors = "There's an error while deleting product image/s." });
        }
        #endregion

        #region DELETE Delete Product
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteProduct(ProductDeleteRequestDto product)
        {
            // TODO: Check roles
            // TODO: Fix CreatedBy
            var result = await _productService.DeleteProductAsync(product);
            return !result ? BadRequest(new { Errors = new[] { "Failed to delete product." } }) : Ok();
        }
        #endregion

        #region PRIVATE Get Image Path
        private string GetImagePath()
        {
            var request = HttpContext.Request;
            var appUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            return appUrl + "/Images/Products/";
        }
        #endregion
    }
}
