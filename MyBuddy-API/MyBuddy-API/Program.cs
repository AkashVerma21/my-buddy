using MyBuddy_API.Extensions;
using MyBuddy_API.Data; // For ApplicationDbContext

var builder = WebApplication.CreateBuilder(args);

// --- Add services to the container ---

// 1. Configure Persistence (Database)
builder.Services.AddPersistence(builder.Configuration);

// 2. Configure Authentication
builder.Services.AddJwtAuthentication(builder.Configuration);

// 3. Configure API Versioning
builder.Services.AddApiVersioningConfiguration();

// 4. Configure Swagger
builder.Services.AddSwaggerDocumentation();

// 5. Configure CORS
builder.Services.AddCors(options =>
{
    // WARNING: "AllowAnyOrigin" is insecure for production.
    // Lock this down to specific origins.
    options.AddPolicy("AllowAnyOrigin", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 6. Add standard ASP.NET Core services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// --- Build the application ---
var app = builder.Build();

// --- Configure the HTTP request pipeline ---

// Use Swagger only in development environments for security
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();

    // Optional: Apply migrations automatically in development.
    // Do NOT do this in production. Use a CI/CD pipeline for migrations.
    app.ApplyMigrations();
}

app.UseHttpsRedirection(); // Always a good practice

app.UseCors("AllowAnyOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();