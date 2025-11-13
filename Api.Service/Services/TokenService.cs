using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Service.Interfaces;
using Api.Service.Settings;
using Api.Service.Dtos;


namespace Api.Service.Services;


public class TokenService(JwtSettings jwtSettings) : ITokenService
{
    private JwtSettings JwtSettings { get; init; } = jwtSettings;


    public string GenrateToken(UserResultDto user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = BuildClaims(user);

        var token = new JwtSecurityToken(
            issuer: JwtSettings.Issuer,
            audience: JwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(JwtSettings.ExpiresInMinutes)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    private static Claim[] BuildClaims(UserResultDto user)
    {
        return
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToString())
        ];
    }
}
