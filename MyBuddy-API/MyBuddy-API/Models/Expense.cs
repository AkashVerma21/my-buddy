using System.ComponentModel.DataAnnotations;

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

    }
}
