using System.Net;


namespace Api.Service.Exceptions;


public class InternalServerException : BaseServiceException
{
    public InternalServerException() : base(
        HttpStatusCode.InternalServerError,
        "Um erro desconhecido ocorreu ao processar a requisição."
    )
    { }


    public static InternalServerException Build()
    {
        return new InternalServerException();
    }
}
