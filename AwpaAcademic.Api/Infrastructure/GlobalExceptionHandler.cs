﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AwpaAcademic.Api.Infrastructure;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Server error"
        };

        switch (exception)
        {
            case InvalidOperationException invalidOpEx:
                problemDetails.Status = StatusCodes.Status409Conflict;
                problemDetails.Title = "Conflict";
                problemDetails.Detail = invalidOpEx.Message;
                break;

            default:
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Title = "Server error";
                problemDetails.Detail = "An unexpected error occurred. Please try again later.";
                break;
        }

        problemDetails.Type = "https://tools.ietf.org/html/rfc9110#section-15.5.5";
        problemDetails.Status = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
        problemDetails.Title = problemDetails.Title ?? "An unexpected error occurred.";

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
