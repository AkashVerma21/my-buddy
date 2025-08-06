using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace MyBuddy_API.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An umhandled exception has occured: {ex.Message}");

                await HandleExceptionAsync(httpContext,ex);
            }

        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "An internal server error has occurred. Please try again later.",
                Error = Guid.NewGuid().ToString()
            };

            var jsonResponse= JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
