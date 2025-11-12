using Microsoft.IdentityModel.Tokens;


namespace Api.Repository.Entities;


public class User
{
    public virtual long Id { get; protected set; }
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Surname { get; set; } = string.Empty;
    public virtual string Email { get; set; } = string.Empty;
    public virtual string Password { get; protected set; } = string.Empty;
    public virtual string PhoneNumber { get; set; } = string.Empty;
    public virtual DateTime BirthDate { get; set; }


    public User() { }



    public virtual void SetPassword(string plainTextPassword)
    {
        Password = BCrypt.Net.BCrypt.HashPassword(plainTextPassword);
    }


    public virtual bool VerifyPassword(string plainTextPassword)
    {
        if (Password.IsNullOrEmpty() || plainTextPassword.IsNullOrEmpty())
        {
            return false;
        }
        return BCrypt.Net.BCrypt.Verify(plainTextPassword, Password);
    }
}
