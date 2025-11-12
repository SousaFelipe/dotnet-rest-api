using System.Net;


namespace Api.Service.Exceptions;


public class RecordUpdateException : BaseServiceException
{
    public RecordUpdateException() : base(
        HttpStatusCode.Conflict,
        "Um erro desconhecido ocorreu ao tentar atalizar o registro no banco de dados."
    ) {}
}
