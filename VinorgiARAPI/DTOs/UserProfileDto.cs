namespace VinorgiARAPI.DTOs
{
    public class UserProfileDto
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public int TotalModels { get; set; }
    }
}
