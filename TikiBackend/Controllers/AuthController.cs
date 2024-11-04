using BookCatalog.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly string _jwtSecret = "my_test_secret_key_123!12131221421"; // Make sure to use a strong secret

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = UserStore.Users.Find(u => u.Username == request.Username && u.Password == request.Password);
            // Authenticate the user (replace this with your authentication logic)
            if (user.UserId != null && user.UserId != 0) // Example credentials
            {
                // Retrieve this from your user data
                var token = GenerateJwtToken(user.UserId.ToString());

                // Optionally, return user profile information if needed
                var userProfile = new { user.UserId, username = request.Username }; // Add more fields as needed

                return Ok(new { token = token, profile = userProfile, role = user.Role });
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // In a stateless JWT scenario, you usually don't need server-side logout logic
            return Ok("Logged out successfully");
        }

        private string GenerateJwtToken(string userId)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null, // Set the issuer if necessary
                audience: null, // Set the audience if necessary
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
