using System.ComponentModel.DataAnnotations;

namespace MyBuddy_API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }

        // Navigation Property - One User has Many Expenses
        public ICollection<Expense> Expenses { get; set; }
    }
}
