using Merchandise.Application.Dtos.Requests.Brand;
using Merchandise.Application.Interfaces;
using Merchandise.Application.Services;
using Merchandise.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Merchandise.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        public readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetBrands()
        {
            var result = await _brandService.GetActiveBrandsAsync();
            return Ok(result);
        }

        [HttpGet("Detail/{id:guid}")]
        public async Task<IActionResult> GetDetail(Guid id)
        {
            var result = await _brandService.GetBrandAsync(id);
            return result is not null ? Ok(result) : NotFound();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddBrand(BrandAddRequestDto Brand)
        {
            var result = await _brandService.AddBrandAsync(Brand);
            return result.Status == RequestResponseStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateBrand(BrandUpdateRequestDto Brand)
        {
            var result = await _brandService.UpdateBrandAsync(Brand);
            return result.Status == RequestResponseStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteBrand([FromBody] BrandDeleteRequestDto Brand)
        {
            // TODO: Fix the response
            var result = await _brandService.DeleteBrandAsync(Brand);
            return result.Status == RequestResponseStatus.Success ? Ok(result) : BadRequest(result);
        }
    }
}
