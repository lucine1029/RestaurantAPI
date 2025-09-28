namespace RestaurantAPI.Services.IServices
{
    public interface IJWTService
    {
        string GenerateToken(string username, string role);
        string ValidateToken(string token);
        
    }
}
