using Api.Service.Dtos;
using Api.Service.Exceptions;
using Api.Service.Interfaces;


namespace Api.Service.Services;


public class AuthService(ITokenService tokenService, IUserService userService) : IAuthService
{
    private ITokenService TokenService { get; init; } = tokenService;
    private IUserService UserService { get; init; } = userService;


    public async Task<AuthResultDto> TryToLogin(AuthRequestDto authDto)
    {
        if (authDto.Email == null || authDto.Password == null)
        {
            throw new AuthMissingCredentialsException();
        }

        var user = await UserService.FindUser("email", authDto.Email)
            ?? throw new AuthInvalidCredentialsException();

        if (!user.VerifyPassword(authDto.Password))
        {
            throw new AuthInvalidCredentialsException();
        }

        var token = TokenService.GenrateToken(user);
        return new AuthResultDto(token);
    }
}
