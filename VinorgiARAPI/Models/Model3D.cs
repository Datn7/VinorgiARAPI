using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinorgiARAPI.Models
{
    [Table("Models")]
    public class Model3D
    {
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FileUrl { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
