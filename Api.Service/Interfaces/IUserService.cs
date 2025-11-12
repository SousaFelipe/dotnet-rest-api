using Api.Data.Entities;


namespace Api.Service.Interfaces;


public interface IUserService : IService
{
    public Task<User> CreateUser(User user);


    public Task<User?> ReadUser(long userId);


    public Task<IEnumerable<User>> ReadPagedUsers(int page, int size);


    public Task<User> UpdateUser(long userId, User user);


    public Task<bool> DeleteUser(long userId);
}
