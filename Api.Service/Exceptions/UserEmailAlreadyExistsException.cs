using System.Net;


namespace Api.Service.Exceptions;


public class UserEmailAlreadyExistsException(string email)
    : BaseServiceException(
        HttpStatusCode.Conflict,
        $"O email {email} já está em uso"
    ) { }
