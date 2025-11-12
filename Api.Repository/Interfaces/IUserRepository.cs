using Api.Repository.Entities;


namespace Api.Repository.Interfaces;


public interface IUserRepository
{
    public Task<User?> CreateUser(User user);


    public Task<User?> ReadUser(long userId);


    public Task<List<User>> ReadPagedUsers(int page, int size);


    public Task<User> UpdateUser(long userId, User user);


    public Task<bool> DeleteUser(long userId);
}
