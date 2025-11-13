namespace Api.Service.Dtos;


public class AuthResultDto
{
    public string Token { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; }
}
