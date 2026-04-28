using Chiro.Application.Dtos;
using Chiro.Application.Interfaces;
using Chiro.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Chiro.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpGet("all-users")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await authService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("group-leaders")]
        public async Task<ActionResult<IEnumerable<User>>> GetGroupLeaders()
        {
            var groupLeaders = await authService.GetGroupLeadersAsync();
            return Ok(groupLeaders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            var user = await authService.GetUserByIdAsync(id);
            if (user is null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var user = await authService.RegisterAsync(request);
            if (user is null)
            {
                return BadRequest("Username already exists.");
            }
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
            var response = await authService.LoginAsync(request);
            if (response is null)
            {
                return BadRequest("Invalid username or password.");
            }

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await authService.RefreshTokensAsync(request);
            if (result is null || result.AccesToken is null || result.RefreshToken is null)
                return Unauthorized("Invalid refresh token.");

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated.");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("You are admin.");
        }

        [HttpPut("ModifyUserDetails")]
        public async Task<ActionResult<UserShortDto>> ModifyDetails(UserShortDto request)
        {
            var result = await authService.ModifyDetailsAsync(request);
            if (result is null)
                return BadRequest("Failed to add user details.");
            return Ok(result);

        }

        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto request)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await authService.ChangePasswordAsync(userId, request);
            if (!result) return BadRequest("Huidig wachtwoord is onjuist.");
            return Ok();
        }

        [Authorize]
        [HttpPost("profile-picture")]
        public async Task<IActionResult> UploadProfilePicture(IFormFile file)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var url = await authService.UploadProfileImageAsync(userId, file);

            return Ok(url);
        }
    }
}
