namespace Api.Data.Entities;


public class User
{
    public virtual long Id { get; protected set; }
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Surname { get; set; } = string.Empty;
    public virtual string Email { get; set; } = string.Empty;
    public virtual string Password { get; set; } = string.Empty;
    public virtual string PhoneNumber { get; set; } = string.Empty;
    public virtual DateOnly BirthDate { get; set; }


    public User() { }
}
