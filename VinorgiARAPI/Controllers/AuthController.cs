using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VinorgiARAPI.DTOs;
using VinorgiARAPI.Models;
using VinorgiARAPI.Services;

namespace VinorgiARAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenService _tokenService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // ← Add this for debugging

            var user = new ApplicationUser
            {
                UserName = dto.Username,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { token = _tokenService.CreateToken(user) });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user == null)
                    return Unauthorized("Invalid email");

                var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
                if (!result.Succeeded)
                    return Unauthorized("Invalid password");

                return Ok(new
                {
                    token = _tokenService.CreateToken(user),
                    user = new
                    {
                        id = user.Id,
                        email = user.Email,
                        displayName = user.DisplayName,
                        profileImageUrl = user.ProfileImageUrl
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Login error: " + ex.Message);
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
    }
}
