using Api.Data.Entities;


namespace Api.Service.Interfaces;


public interface IUserService : IService
{
    public Task<IEnumerable<User>> ReadUsers();


    public Task<User> CreateUser(User user);


    public Task<User> UpdateUser(User user);


    public Task<bool> DeleteUser(Guid id);
}
