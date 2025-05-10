using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBuddy_API.Models
{
    public class Expense
    {
        [Key]
        //[JsonIgnore] don't it will ignore in every json request but i want to use for only post request. 
        public int Id { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Category { get; set; }

        // Foreign key to associate the expense with a user
        [Required]
        public int UserId { get; set; }

        // Navigation property for the related user
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
