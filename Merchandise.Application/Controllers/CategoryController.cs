using Merchandise.Application.Dtos.Requests.Category;
using Merchandise.Application.Interfaces;
using Merchandise.Domain.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Merchandise.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _categoryService.GetActiveCategoriesAsync();
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddCategory(CategoryAddRequestDto category)
        {
            var result = await _categoryService.AddCategoryAsync(category);
            return result.Status == RequestResponseStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateRequestDto category)
        {
            var result = await _categoryService.UpdateCategoryAsync(category);
            return result.Status == RequestResponseStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            // TODO: Fix the response
            var result = await _categoryService.DeleteCategoryAsync(id);
            return result.Status == RequestResponseStatus.Success ? Ok(result) : BadRequest(result);
        }
    }
}
