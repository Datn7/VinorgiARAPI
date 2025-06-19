using Microsoft.AspNetCore.Identity;

namespace VinorgiARAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Model3D> Models { get; set; }
    }
}
