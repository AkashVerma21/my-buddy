// Extensions/ApiVersioningExtensions.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace MyBuddy_API.Extensions
{
    public static class ApiVersioningExtensions
    {
        public static IServiceCollection AddApiVersioningConfiguration(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                // Read version from the URL segment (e.g., /api/v1/...)
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddVersionedApiExplorer(options =>
            {
                // Format the version as "v1", "v2", etc.
                options.GroupNameFormat = "'v'VVV";
                // Substitute the API version in the URL path
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}