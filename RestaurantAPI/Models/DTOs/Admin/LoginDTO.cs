namespace RestaurantAPI.Models.DTOs.Admin
{
    public class LoginDTO
    {
        public string? Username { get; set; }
        public string? AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
