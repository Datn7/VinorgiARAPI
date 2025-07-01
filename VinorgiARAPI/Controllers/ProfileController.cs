using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VinorgiARAPI.Data;
using VinorgiARAPI.DTOs;
using VinorgiARAPI.Models;

namespace VinorgiARAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;

        public ProfileController(UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<UserProfileDto>> GetProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var modelCount = await _context.Models3D.CountAsync(m => m.UserId == user.Id);

            var dto = new UserProfileDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName ?? "",
                ProfileImageUrl = user.ProfileImageUrl,
                TotalModels = modelCount
            };

            return Ok(dto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile(UserProfileDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            user.DisplayName = dto.DisplayName;

            if (user.Email != dto.Email)
            {
                user.Email = dto.Email;
                user.UserName = dto.Email;
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }
    }
}
