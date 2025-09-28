using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Data.Repositories.IRepositories;
using RestaurantAPI.Models.DTOs.Auth;
using RestaurantAPI.Services.IServices;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //AuthController is only for handling authentication-related endpoints like login, register, token refresh, etc.
        private readonly IAdminRepo _adminRepo;
        private readonly IJWTService _jwtService;
        public AuthController(IAdminRepo adminRepo, IJWTService jwtService)
        {
            _adminRepo = adminRepo;
            _jwtService = jwtService;
        }


        [HttpPost]
        [Route("/login")]
        public async Task<ActionResult> Login([FromQuery] LoginDTO loginDTO)
        {
            var admin = await _adminRepo.GetAdminByUsernameAsync(loginDTO.Username);
            if (admin == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, admin.PasswordHash))
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }
            var token = _jwtService.GenerateToken(admin);
            var loginResponse = new LoginResponseDTO
            {

                Token = token,
                Username = admin.Username,
                Role = admin.Role

            };
            return Ok(loginResponse);
        }
    }
}
