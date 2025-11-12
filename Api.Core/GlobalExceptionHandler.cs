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
            return await TryWriteResponseWithException(serviceException.GetDetails(), httpContext, cancellationToken);
        }

        var defaultException = InternalServerException.Build();

        httpContext.Response.StatusCode = (int)defaultException.StatusCode;
        return await TryWriteResponseWithException(defaultException.GetDetails(), httpContext, cancellationToken);
    }


    private static async Task<bool> TryWriteResponseWithException(
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
