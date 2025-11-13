using Api.Repository.Entities;


namespace Api.Service.Interfaces;


public interface ITokenService
{
    public string GenrateToken(User user);
}
