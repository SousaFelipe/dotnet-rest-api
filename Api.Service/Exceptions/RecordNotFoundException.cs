using System.Net;


namespace Api.Service.Exceptions;


public class RecordNotFoundException : BaseServiceException
{
    public RecordNotFoundException() : base(
        HttpStatusCode.NotFound,
        "O registro que você está buscando não existe no banco de dados."
    )
    { }
}
