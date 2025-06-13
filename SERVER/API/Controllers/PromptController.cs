using BLL.API;
using BLL.DTOs;
using BLL.Services;
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
        public async Task<ActionResult<PromptDTO>> GetPromptById(int id)
        {
            var prompt = await _promptService.GetByIdAsync(id);
            if (prompt == null)
                return NotFound();
            return Ok(prompt);
        }

        [HttpPost("process prompt")]
        public async Task<IActionResult> CreatePrompt([FromBody] PromptDTO promptDto, [FromQuery] int userId)
        {
            var response = await _promptService.ProcessPromptAsync(promptDto, userId);
            return Ok(response);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetUserHistory([FromQuery] int userId)
        {
            var history = await _promptService.GetUserHistoryAsync(userId);
            return Ok(history);
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
