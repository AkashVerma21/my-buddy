using Microsoft.EntityFrameworkCore;
using MyBuddy_API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
    //.AddJsonOptions(options =>
    //{
    //    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    //});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Use the IApiVersionDescriptionProvider service to get API version info
    var apiVersionDescriptionProvider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
    {
        options.SwaggerDoc(description.GroupName, new OpenApiInfo
        {
            Title = $"My Awesome API {description.ApiVersion}",
            Version = description.ApiVersion.ToString(),
            Description = description.IsDeprecated ? "This API version has been deprecated." : ""
        });
    }
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true; // This will include the API version in the response headers
    options.AssumeDefaultVersionWhenUnspecified = true; // This will assume the default version if not specified
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0); // Set the default API version
    options.ApiVersionReader=new UrlSegmentApiVersionReader(); // This will read the API version from the URL segment
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // This will format the version as v1, v2, etc.
    options.SubstituteApiVersionInUrl = true; // This will substitute the API version in the URL
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        policy =>
        {
            policy.AllowAnyOrigin() // Replace with the origin you want to allow
                 .AllowAnyMethod()
                 .AllowAnyHeader();
        });
});

// Configure PostgreSQL with Npgsql
//builder.Services.AddDbContext<ExpenseContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDbContext<UserContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var connectionString = Environment.GetEnvironmentVariable("DefaultConnection") ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Read JWT settings from configuration

//var jwtSettings = builder.Configuration.GetSection("Jwt");
//var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("Jwt__Key") ?? jwtSettings["Key"]);
var issuer = Environment.GetEnvironmentVariable("Jwt__Issuer") ?? jwtSettings["Issuer"];
var audience = Environment.GetEnvironmentVariable("Jwt__Audience") ?? jwtSettings["Audience"];


// Create a logger factory and use it to create the logger
var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder.AddConsole());
var logger = loggerFactory.CreateLogger("EnvironmentLogger");

logger.LogInformation("JWT Key: {Key}", key);
logger.LogInformation("JWT Issuer: {Issuer}", issuer);
logger.LogInformation("JWT Audience: {Audience}", audience);
logger.LogInformation("Connection String: {ConnectionString}", connectionString);

// Ensure the key length is sufficient
if (key.Length < 32)
{
    throw new ArgumentException("The JWT key must be at least 32 characters long.");
}

// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = issuer,
        ValidAudience = audience
    };
});

var app = builder.Build();

// Apply any pending migrations and create the database if it does not exist
using (var scope = app.Services.CreateScope())
{
    //var dbContext = scope.ServiceProvider.GetRequiredService<ExpenseContext>();
    //dbContext.Database.Migrate();

    //var userContext = scope.ServiceProvider.GetRequiredService<UserContext>();
    //userContext.Database.Migrate();

    var applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    applicationContext.Database.Migrate();

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}


// Use the permissive CORS policy
app.UseCors("AllowAnyOrigin");

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
