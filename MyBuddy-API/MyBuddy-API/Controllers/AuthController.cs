using Microsoft.AspNetCore.Mvc;
using MyBuddy_API.Data;
using MyBuddy_API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyBuddy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(UserContext context, IConfiguration configuration)
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

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, existingUser.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Audience = _configuration["Jwt:Audience"], // Add the audience claim
                Issuer = _configuration["Jwt:Issuer"],    // Add the issuer claim
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }
    }
}
