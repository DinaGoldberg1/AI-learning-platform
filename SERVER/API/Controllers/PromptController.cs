using BLL.API;
using BLL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromptController : ControllerBase
    {
        private readonly IPromptServiceBLL _promptService;

        public PromptController(IPromptServiceBLL promptService)
        {
            _promptService = promptService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PromptDTO>>> GetPrompts()
        {
            var prompts = await _promptService.GetAllAsync();
            return Ok(prompts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PromptDTO>> GetPrompt(int id)
        {
            var prompt = await _promptService.GetByIdAsync(id);
            if (prompt == null)
                return NotFound();
            return Ok(prompt);
        }

        [HttpPost]
        public async Task<ActionResult<PromptDTO>> CreatePrompt(PromptDTO promptDto)
        {
            var createdPrompt = await _promptService.CreateAsync(promptDto);
            return CreatedAtAction(nameof(GetPrompt), new { id = createdPrompt.Id }, createdPrompt);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrompt(int id, PromptDTO promptDto)
        {
            var updated = await _promptService.UpdateAsync(id, promptDto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrompt(int id)
        {
            var deleted = await _promptService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
