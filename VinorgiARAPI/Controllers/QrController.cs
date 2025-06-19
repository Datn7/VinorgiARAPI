using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;
using VinorgiARAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace VinorgiARAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QrController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public QrController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet("{modelId}")]
        public async Task<IActionResult> GenerateQr(int modelId)
        {
            var model = await _context.Models.FirstOrDefaultAsync(m => m.Id == modelId);

            if (model == null)
                return NotFound("Model not found.");

            var baseUrl = _config["AppSettings:ViewerBaseUrl"]; // e.g. http://localhost:4200/ar-viewer
            var qrContent = $"{baseUrl}?modelId={model.Id}";

            using var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);

            using var pngQrCode = new PngByteQRCode(qrCodeData);
            var qrBytes = pngQrCode.GetGraphic(20);
            return File(qrBytes, "image/png");
        }
    }
}
