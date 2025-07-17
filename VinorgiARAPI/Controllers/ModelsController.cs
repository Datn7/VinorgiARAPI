using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VinorgiARAPI.Data;

namespace VinorgiARAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ModelsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _context.Models
                .Select(m => new
                {
                    m.Id,
                    m.FileUrl,
                    m.UploadedAt
                })
                .ToListAsync();

            return Ok(models);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            var models = await _context.Models
                .Where(m => m.UserId == userId)
                .Select(m => new {
                    m.Id,
                    m.FileUrl,
                    m.FileName,
                    m.UploadedAt
                })
                .ToListAsync();

            return Ok(models);
        }

    }
}
