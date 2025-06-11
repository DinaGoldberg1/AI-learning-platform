using BLL.API;
using BLL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryServiceBLL _subCategoryService;

        public SubCategoryController(ISubCategoryServiceBLL subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SubCategoryDTO>>> GetSubCategories()
        {
            var subCategories = await _subCategoryService.GetAllAsync();
            return Ok(subCategories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategoryDTO>> GetSubCategory(int id)
        {
            var subCategory = await _subCategoryService.GetByIdAsync(id);
            if (subCategory == null)
                return NotFound();
            return Ok(subCategory);
        }

        [HttpPost]
        public async Task<ActionResult<SubCategoryDTO>> CreateSubCategory(SubCategoryDTO subCategoryDto)
        {
            var createdSubCategory = await _subCategoryService.CreateAsync(subCategoryDto);
            return CreatedAtAction(nameof(GetSubCategory), new { id = createdSubCategory.Id }, createdSubCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubCategory(int id, SubCategoryDTO subCategoryDto)
        {
            var updated = await _subCategoryService.UpdateAsync(id, subCategoryDto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            var deleted = await _subCategoryService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
