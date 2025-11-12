using System.Net;


namespace Api.Service.Exceptions;


public class RecordCreationException : BaseServiceException
{
    public RecordCreationException() : base(
        HttpStatusCode.InternalServerError,
        "Um erro desconhecido ocorreu ao tentar salvar o registro no banco de dados."
    )
    { }
}
