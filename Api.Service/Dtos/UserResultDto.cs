using Microsoft.IdentityModel.Tokens;
using Api.Repository.Entities;


namespace Api.Service.Dtos;


public class UserResultDto(User user)
{
    public long Id { get; set; } = user.Id;
    public string Name { get; set; } = user.Name;
    public string Surname { get; set; } = user.Surname;
    public string Email { get; set; } = user.Email;
    public string PhoneNumber { get; set; } = user.PhoneNumber;
    public DateOnly BirthDate { get; set; } = DateOnly.FromDateTime(user.BirthDate);
    private string Password { get; set; } = user.Password;


    public virtual bool VerifyPassword(string plainTextPassword)
    {
        if (Password.IsNullOrEmpty() || plainTextPassword.IsNullOrEmpty())
        {
            return false;
        }
        return BCrypt.Net.BCrypt.Verify(plainTextPassword, Password);
    }
}
