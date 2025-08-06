namespace MyBuddy_API.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log request
            await LogRequest(context);

            // Capture response
            var originalBodyStream = context.Response.Body;
            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            // Continue down the pipeline
            await _next(context);

            // Log response
            await LogResponse(context, responseBody);

            // Copy the response stream back to the original stream
            await responseBody.CopyToAsync(originalBodyStream);
        }

        private async Task LogRequest(HttpContext context)
        {
            // This line is crucial for reading the request body multiple times.
            context.Request.EnableBuffering();

            // Read the request body
            var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            var requestBody = await reader.ReadToEndAsync();

            // Reset the request body stream position so the next middleware can read it
            context.Request.Body.Position = 0;

            _logger.LogInformation(
                "Incoming Request: {Method} {Path} | Body: {Body}",
                context.Request.Method,
                context.Request.Path,
                requestBody);
        }

        private async Task LogResponse(HttpContext context, MemoryStream responseBody)
        {
            // Reset the stream position to read from the beginning
            responseBody.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();

            // Reset the stream position again so it can be copied to the original stream
            responseBody.Seek(0, SeekOrigin.Begin);

            _logger.LogInformation(
                "Outgoing Response: {StatusCode} | Body: {Body}",
                context.Response.StatusCode,
                responseBodyText);
        }
    }
}
