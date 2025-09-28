using RestaurantAPI.Services.IServices;

namespace RestaurantAPI.Services
{
    public class JWTService : IJWTService
    {
        public string GenerateToken(string username, string role)
        {
            throw new NotImplementedException();
        }

        public string ValidateToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
