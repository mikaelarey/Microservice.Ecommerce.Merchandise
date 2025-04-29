using Merchandise.Application.Dtos.Requests.Product;
using Merchandise.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        #region AddProduct
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

        #region GetProducts
        [HttpGet("Get")]
        public async Task<IActionResult> GetProducts(int pageNo)
        {
            var imagePath = GetImagePath();
            var result = await _productService.GetProductsAsync(pageNo, imagePath);
            return Ok(result);
        }
        #endregion

        #region GetProductById
        [HttpGet("Detail/{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var imagePath = GetImagePath();
            var result = await _productService.GetProductByIdAsync(id, imagePath);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        #endregion

        #region SearchProducts
        [HttpPost("Search")]
        public async Task<IActionResult> SearchProducts(ProductSearchRequestDto request)
        {
            var result = await _productService.SearchProductsAsync(request);
            return Ok(result);
        }
        #endregion

        
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProductDetail(ProductUpdateRequestDto request)
        {
            // TODO: Check roles
            // TODO: Fix CreatedBy
            var result = await _productService.UpdateProductDetailAsync(request);

            return (result.ErrorMessages is not null)
                ? BadRequest(result)
                : Ok(result);
        }

        #region AddProductImages
        [HttpPut("AddImages")]
        public async Task<IActionResult> AddProductImages(ProductImageAddRequestDto request)
        {
            // TODO: Check roles
            // TODO: Fix CreatedBy
            var imagePath = GetImagePath();
            var result = await _productService.AddProductImagesAsync(request, imagePath);
            return Ok(result);
        }
        #endregion

        #region DeleteProductImages
        [HttpPut("DeleteImages")]
        public async Task<IActionResult> DeleteProductImages(ProductImageDeleteRequestDto request)
        {
            // TODO: Check roles
            // TODO: Fix CreatedBy
            var result = await _productService.DeleteProductImagesAsync(request);

            if (result.DeletedImageIds is not null && result.DeletedImageIds.Any())
            {
                return Ok(result);
            }

            return BadRequest(new { Errors = "There's an error while deleting product image/s." });
        }
        #endregion

        #region SetPrimaryProductImages
        [HttpPut("SetPrimaryImage")]
        public async Task<IActionResult> SetPrimaryProductImages(ProductUpdatePrimaryImageRequestDto request)
        {
            // TODO: Check roles
            // TODO: Fix CreatedBy
            var result = await _productService.SetPrimaryProductImagesAsync(request);

            if (!result)
            {
                return BadRequest(new { Errors = new[] { "Failed to set primary image." } });
            }

            return Ok();
        }
        #endregion

        #region GetImagePath
        private string GetImagePath()
        {
            var request = HttpContext.Request;
            var appUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            return appUrl + "/Images/Products/";
        }
        #endregion
    }
}
