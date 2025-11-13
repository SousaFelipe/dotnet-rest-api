namespace Api.Service.Dtos;


public class AuthResult(string token)
{
    public string Token { get; set; } = token;
}
