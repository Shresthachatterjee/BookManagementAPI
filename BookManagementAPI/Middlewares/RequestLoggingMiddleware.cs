using System.Diagnostics;

/// <summary>
/// Middleware to log incoming HTTP requests with method, path, status code, and response time.
/// Useful for monitoring application performance.
/// </summary>
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<RequestLoggingMiddleware> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="RequestLoggingMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="logger">The logger to log request information.</param>
    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    /// <summary>
    /// Middleware method invoked for each HTTP request. Logs the request method, path, response status, and execution time.
    /// </summary>
    /// <param name="context">HTTP context of the request.</param>
    /// <returns>A task that represents the completion of request processing.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        // Log before request is processed
        logger.LogWarning("➡️ Middleware START: {method} {url}", context.Request.Method, context.Request.Path);

        var stopwatch = Stopwatch.StartNew();

        // Proceed with the request
        await this.next(context);

        stopwatch.Stop();
        var elapsedMs = stopwatch.ElapsedMilliseconds;

        // Log after request is processed
        logger.LogWarning("✅ Middleware END: {method} {url} responded {statusCode} in {time} ms",
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode,
            elapsedMs);

        // Log as information for long-term monitoring
        logger.LogInformation(
            "Request: {method} {url} responded {statusCode} in {time} ms",
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode,
            elapsedMs);
    }
}
