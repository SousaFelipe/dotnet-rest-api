using Api.Repository.Entities;
using Api.Repository.Interfaces;
using Api.Service.Dtos;
using Api.Service.Exceptions;
using Api.Service.Interfaces;


namespace Api.Service.Services;


public class AuthService(
    IRepository<User> userRepository,
    ITokenService tokenService) : IAuthService
{
    private IRepository<User> UserRepository { get; init; } = userRepository;
    private ITokenService TokenService { get; init; } = tokenService;


    public async Task<AuthResult> TryToLogin(AuthRequestCredentials authDto)
    {
        if (authDto.Email == null || authDto.Password == null)
        {
            throw new AuthMissingCredentialsException();
        }

        var user = await UserRepository.FindBy("email", authDto.Email)
            ?? throw new AuthInvalidCredentialsException();

        if (!user.VerifyPassword(authDto.Password))
        {
            throw new AuthInvalidCredentialsException();
        }

        var userResult = new UserResult(user);
        var token = TokenService.GenrateToken(userResult);
        
        return new AuthResult(token);
    }
}
