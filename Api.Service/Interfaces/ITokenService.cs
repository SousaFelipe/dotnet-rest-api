using Api.Service.Dtos;


namespace Api.Service.Interfaces;


public interface ITokenService
{
    public string GenrateToken(UserResultDto user);
}
