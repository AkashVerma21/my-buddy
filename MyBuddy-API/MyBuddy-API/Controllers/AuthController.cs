using Microsoft.AspNetCore.Mvc;
using MyBuddy_API.Data;
using MyBuddy_API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MyBuddy_API.DTO;
using Google.Apis.Auth;
using User = MyBuddy_API.Models.User;
namespace MyBuddy_API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(UserDto userDto)
        {
            var user = new User
            {
                Username = userDto.Username,
                Password = userDto.Password // In a real application, hash the password before saving
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("signin")]
        public async Task<IActionResult> Signin(UserDto user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            if (existingUser == null)
                return Unauthorized(new { message = "Invalid credentials" });
            string tokenString = GenerateJwtToken(existingUser.Username);

            return Ok(new { Token = tokenString });
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);
            }
            catch
            {
                return Unauthorized("Invalid Google token");
            }

            // If new user to database if needed
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == payload.Email);
            if (existingUser == null)
            {
                var newUser = new User
                {
                    Username = payload.Email,
                    Password = Guid.NewGuid().ToString() // Generate a random password or handle as needed
                };
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
            }

            string tokenString = GenerateJwtToken(payload.Email);

            return Ok(new { Token = tokenString });
        }

        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Audience = _configuration["Jwt:Audience"], // Add the audience claim
                Issuer = _configuration["Jwt:Issuer"],    // Add the issuer claim
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }

    public class GoogleLoginRequest
    {
        public string IdToken { get; set; }
    }
}