using Api.Service.Dtos;

namespace Api.Service.Interfaces;


public interface IAuthService
{
    public Task<AuthResultDto> TryToLogin(AuthRequestDto authDto);
}
