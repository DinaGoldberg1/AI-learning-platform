using BLL.API;
using BLL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServiceBLL _categoryService;
        private readonly ISubCategoryServiceBLL _subCategoryService;

        public CategoryController(ICategoryServiceBLL categoryService, ISubCategoryServiceBLL subCategoryService)
        {
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpGet("{id}/subcategories")]
        public async Task<ActionResult<List<SubCategoryDTO>>> GetSubCategoriesByCategoryId(int id)
        {
            var subCategories = await _categoryService.GetSubCategoriesByIdAsync(id);
            return Ok(subCategories);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CategoryDTO categoryDto)
        {
            var createdCategory = await _categoryService.CreateAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDTO categoryDto)
        {
            var updated = await _categoryService.UpdateAsync(id, categoryDto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleted = await _categoryService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
