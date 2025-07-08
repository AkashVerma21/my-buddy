// Extensions/PersistenceExtensions.cs

using Microsoft.EntityFrameworkCore;
using MyBuddy_API.Data; // Assuming this is your DbContext namespace

namespace MyBuddy_API.Extensions
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("DefaultConnection")
                                   ?? configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Database connection string 'DefaultConnection' not found.");
            }

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            return services;
        }

        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            // WARNING: This is not recommended for production environments.
            // It's better to handle migrations as part of your CI/CD pipeline.
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}