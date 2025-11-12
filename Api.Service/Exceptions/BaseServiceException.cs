using System.Net;
using Microsoft.AspNetCore.Mvc;


namespace Api.Service.Exceptions;


public abstract class BaseServiceException(HttpStatusCode statusCode, string message)
    : Exception(message)
{
    public HttpStatusCode StatusCode { get; private init; } = statusCode;


    public ProblemDetails GetDetails()
    {
        return new ProblemDetails
        {
            Status = (int)StatusCode,
            Title = StatusCode.ToString(),
            Detail = Message
        };
    }
}
