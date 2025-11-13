using System.Net;

namespace Api.Service.Exceptions;


public class AuthInvalidCredentialsException : BaseServiceException
{
    public AuthInvalidCredentialsException() : base(
        HttpStatusCode.Unauthorized,
        "As credenciais de acesso fornecidas estão inválidas."
    ) {}
}
