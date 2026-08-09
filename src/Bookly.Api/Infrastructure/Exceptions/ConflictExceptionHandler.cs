using Bookly.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Bookly.Api.Infrastructure.Exceptions;

public sealed class ConflictExceptionHandler :IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ConflictException conflictException)
            return false;

        var problemDetails = new ProblemDetails
        {
            Title = "Conflict",
            Status = StatusCodes.Status409Conflict,
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.10",
            Detail = conflictException.Error.Message
        };

        problemDetails.Extensions["errors"] = new[] { conflictException.Error };

        httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}