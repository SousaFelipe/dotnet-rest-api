using System.Net;


namespace Api.Service.Exceptions;


public class RecordCreateException : BaseServiceException
{
    public RecordCreateException() : base(
        HttpStatusCode.InternalServerError,
        "Um erro desconhecido ocorreu ao tentar salvar o registro no banco de dados."
    )
    { }
}
