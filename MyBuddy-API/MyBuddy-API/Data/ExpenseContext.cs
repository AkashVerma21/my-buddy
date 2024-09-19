using Microsoft.EntityFrameworkCore;
using MyBuddy_API.Models;

namespace MyBuddy_API.Data
{
    public class ExpenseContext : DbContext
    {
        public ExpenseContext(DbContextOptions<ExpenseContext> options)
            : base(options)
        {
                
        }

        public DbSet<Expense> Expenses { get; set; }
    }
}
