using System.Net;


namespace Api.Service.Exceptions;


public class RecordDeleteException : BaseServiceException
{
    public RecordDeleteException() : base(
        HttpStatusCode.InternalServerError,
        "Um erro desconhecido ocorreu ao tentar remover o registro do banco de dados."
    )
    { }
}
