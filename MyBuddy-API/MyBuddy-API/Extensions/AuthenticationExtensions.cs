
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MyBuddy_API.Models.Settings;

namespace MyBuddy_API.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            // Override with environment variables if they exist
            jwtSettings.Key = Environment.GetEnvironmentVariable("Jwt__Key") ?? jwtSettings.Key;
            jwtSettings.Issuer = Environment.GetEnvironmentVariable("Jwt__Issuer") ?? jwtSettings.Issuer;
            jwtSettings.Audience = Environment.GetEnvironmentVariable("Jwt__Audience") ?? jwtSettings.Audience;

            // Register the settings for dependency injection if needed elsewhere
            services.AddSingleton(jwtSettings);

            var key = Encoding.ASCII.GetBytes(jwtSettings.Key);
            if (key.Length < 32)
            {
                throw new ArgumentException("The JWT key must be at least 256 bits (32 characters) long.");
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // Set to true in production
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    ClockSkew = TimeSpan.Zero // Optional: useful for precise token expiration
                };
            });

            return services;
        }
    }
}