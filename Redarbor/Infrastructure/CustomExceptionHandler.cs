using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Redarbor.Api.Infrastructure;

public sealed class CustomExceptionHandler(
        ILogger<CustomExceptionHandler> logger
    ) : IExceptionHandler
{
    private readonly ILogger<CustomExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger
            .LogError(exception, $"Exception ocurred: {exception.Message}");

        var problemDetail = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error",
            Detail = "An unexpected error ocurred while processing the request.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };

        httpContext.Response.StatusCode = problemDetail.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetail, cancellationToken);

        return true;
    }
}