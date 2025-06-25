using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VinorgiARAPI.Data;
using VinorgiARAPI.Models;

namespace VinorgiARAPI.Controllers
{
    [RequestSizeLimit(100_000_000)] // 100MB example
    [Route("api/models")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public UploadController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadModel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is required.");

            if (!file.FileName.EndsWith(".glb", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Only .glb files are allowed.");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var fileName = $"{Guid.NewGuid()}.glb";
            var savePath = Path.Combine(_env.WebRootPath, "Uploads", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);

            await using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var model = new Model3D
            {
                FileName = fileName,
                FileUrl = $"/Uploads/{fileName}",
                UploadedAt = DateTime.UtcNow,
                UserId = userId
            };

            _context.Models.Add(model);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                model.Id,
                model.FileUrl,
                model.UploadedAt
            });
        }
    }
}
