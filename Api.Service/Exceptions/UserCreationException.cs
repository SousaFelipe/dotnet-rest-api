using System.Net;


namespace Api.Service.Exceptions;


public class UserCreationException : BaseServiceException
{
    public UserCreationException() : base(
        HttpStatusCode.InternalServerError,
        "Um erro desconhecido ocorreu ao tentar salvar os dados do usuário."
    )
    { }
}
