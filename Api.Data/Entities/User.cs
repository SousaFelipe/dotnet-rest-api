namespace Api.Data.Entities;


public sealed class User
{
    public Guid Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }


    public User(Guid? id = null)
    {
        Id = (id == null) ? Guid.NewGuid() : id.Value;
    }
}
