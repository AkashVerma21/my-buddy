using Microsoft.EntityFrameworkCore;
using MyBuddy_API.Models;

namespace MyBuddy_API.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
