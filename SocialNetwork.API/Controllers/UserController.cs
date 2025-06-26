using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.DTOs;
using SocialNetwork.Application.Interfaces;
using SocialNetwork.Application.Payloads;
using SocialNetwork.Shared.Models;
using System.Security.Claims;

namespace SocialNetwork.API.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public async Task<ActionResult<PagedResult<UserSummaryDTO>>> GetUsers([FromQuery] int page = 0, [FromQuery] int size = 10)
        {
            var users = await _userService.GetAllUsersAsync(page, size);
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDTO>> GetUserById([FromRoute] long userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            return Ok(user);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<UserDTO>> UpdateUserById([FromBody] UserUpdateRequest updateRequest)
        {
            var email = User.FindFirstValue(ClaimTypes.Name);
            var updatedUser = await _userService.UpdateUserByEmailAsync(email, updateRequest);
            return Ok(updatedUser);
        }

        [HttpDelete("{userId}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser([FromRoute] long userId)
        {
            await _userService.DeleteUserByIdAsync(userId);
            return NoContent();
        }

        [HttpGet("{userId}/profile-image")]
        public async Task<ActionResult> GetProfileImage([FromRoute] long userId)
        {
            var imageBytes = await _userService.GetProfileImageAsync(userId);

            if (imageBytes == null || imageBytes.Length == 0)
            {
                return NotFound("Image not found");
            }

            return File(imageBytes, "image/jpeg");
        }
    }
}


