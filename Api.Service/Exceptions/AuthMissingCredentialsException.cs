using System.Net;

namespace Api.Service.Exceptions;


public class AuthMissingCredentialsException : BaseServiceException
{
    public AuthMissingCredentialsException() : base(
        HttpStatusCode.Unauthorized,
        "Credenciais auxentes. Certifique-se de enviar o email e a senha corretamente."
    ) {}
}
