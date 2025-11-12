using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Api.Service.Exceptions;


namespace Api.Core;


public class GlobalExceptionHandler() : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is BaseServiceException serviceException)
        {
            httpContext.Response.StatusCode = (int)serviceException.StatusCode;
            return await TryWriteRespondWithException(serviceException.GetDetails(), httpContext, cancellationToken);
        }

        var defaultException = InternalServerException.Build();

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        return await TryWriteRespondWithException(defaultException.GetDetails(), httpContext, cancellationToken);
    }


    private static async Task<bool> TryWriteRespondWithException(
        ProblemDetails details,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        try
        {
            await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
