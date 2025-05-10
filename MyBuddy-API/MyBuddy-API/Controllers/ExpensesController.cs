using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBuddy_API.Data;
using MyBuddy_API.DTO;
using MyBuddy_API.Models;

namespace MyBuddy_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetExpenses()
        {
            //var whatisClaimType=ClaimTypes.NameIdentifier;
            var username = User.Identity.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return Unauthorized();
            }


            var expenses = await _context.Expenses
                        .Where(e => e.UserId == user.Id)
                        .Select(e => new ExpenseDto
                        {
                            Id = e.Id,
                            Description = e.Description,
                            Amount = e.Amount,
                            Date = e.Date,
                            Category = e.Category
                        })
                        .ToListAsync();



            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseDto>> GetExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            var expenseDto = new ExpenseDto
            {
                Id = expense.Id,
                Description = expense.Description,
                Amount = expense.Amount,
                Date = expense.Date,
                Category = expense.Category
            };

            return expenseDto;
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseDto>> PostExpense(CreateExpenseDto expenseDto)
        {
            // Get the authenticated user's username from the JWT token
            var username = User.Identity.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return Unauthorized(new { message = "User not found" });
            }

            var expense = new Expense
            {
                Description = expenseDto.Description,
                Amount = expenseDto.Amount,
                Date = DateTime.SpecifyKind(expenseDto.Date, DateTimeKind.Utc),
                Category = expenseDto.Category,
                UserId = user.Id // Associate the expense with the user
            };
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            var expensedto = new ExpenseDto
            {
                Id = expense.Id,
                Description = expense.Description,
                Amount = expense.Amount,
                Date = expense.Date,
                Category = expense.Category
            };

            return CreatedAtAction("GetExpense", new { id = expense.Id }, expensedto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, ExpenseDto expensedto)
        {
            if (id != expensedto.Id)
            {
                return BadRequest();
            }

            var username = User.Identity.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return Unauthorized(new { message = "User not found" });
            }

            var expense = new Expense
            {
                Id = expensedto.Id,
                Description = expensedto.Description,
                Amount = expensedto.Amount,
                Date = DateTime.SpecifyKind(expensedto.Date, DateTimeKind.Utc),
                Category = expensedto.Category,
                UserId = user.Id
            };

            _context.Entry(expense).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
