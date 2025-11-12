using System.Net;


namespace Api.Service.Exceptions;


public class InvalidRequestException : BaseServiceException
{
    public InvalidRequestException() : base(
        HttpStatusCode.BadRequest,
        "Um ou mais parâmetros obrigatórios estão faltando ou são inválidos."
    )
    { }
}
