namespace Api.Service.Dtos;


public class AuthResultDto(string token)
{
    public string Token { get; set; } = token;
}
