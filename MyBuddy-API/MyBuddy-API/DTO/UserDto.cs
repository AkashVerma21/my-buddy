using System.ComponentModel.DataAnnotations;

namespace MyBuddy_API.DTO
{
    public class UserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
