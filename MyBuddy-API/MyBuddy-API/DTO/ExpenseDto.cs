namespace MyBuddy_API.DTO
{
    public class ExpenseDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Category { get; set; }
    }
}
