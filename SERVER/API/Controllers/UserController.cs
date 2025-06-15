using BLL.API;
using BLL.DTOs;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServiceBLL _userService;

        public UserController(IUserServiceBLL userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(string id)
        {
            var user = await _userService.GetByUserIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser(UserDTO userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.Name))
                return BadRequest("Name is required.");

            var createdUser = await _userService.CreateAsync(userDto);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.UserId}, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, UserDTO userDto)
        {
            if (id != userDto.UserId)
                return BadRequest("ID mismatch.");

            var updated = await _userService.UpdateAsync(id, userDto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var deleted = await _userService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody] UserDTO userDto)
        {
            var user = await _userService.GetByNameAndPhoneAsync(userDto);
            if (user == null)
                return Unauthorized("User not found.");

            return Ok(user);
        }

        [HttpPost("signup")]
        public async Task<ActionResult<UserDTO>> SignUp([FromBody] UserDTO userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.UserId) || string.IsNullOrWhiteSpace(userDto.Phone) || string.IsNullOrWhiteSpace(userDto.Name))
                return BadRequest("All fields are required.");

            var existingUser = await _userService.GetByUserIdAsync(userDto.UserId);
            if (existingUser != null)
                return Conflict("User already exists.");

            await _userService.CreateAsync(userDto);

            return CreatedAtAction(nameof(Login), new { userId = userDto.UserId }, userDto);
        }

    }
}