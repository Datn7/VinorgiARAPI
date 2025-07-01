using Microsoft.AspNetCore.Identity;

namespace VinorgiARAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Model3D> Models { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? DisplayName { get; set; }
    }
}
