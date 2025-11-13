using Api.Service.Dtos;


namespace Api.Service.Interfaces;


public interface IAuthService
{
    public Task<AuthResult> TryToLogin(AuthRequestCredentials authDto);
}
