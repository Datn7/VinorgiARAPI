using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetModel(int id)
        {
            var model = await _context.Models.FindAsync(id);
            if (model == null)
                return NotFound();

            return Ok(new
            {
                model.Id,
                model.FileUrl
            });
        }
    }
}
