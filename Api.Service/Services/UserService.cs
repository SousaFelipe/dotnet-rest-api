using Api.Data.Entities;
using Api.Data.Interfaces;
using Api.Service.Exceptions;
using Api.Service.Interfaces;


namespace Api.Service.Services;


public class UserService(IUserRepository userRepository) : IUserService
{
    private IUserRepository UserRepository { get; init; } = userRepository;


    public async Task<User> CreateUser(User user)
    {
        if (user == null)
        {
            throw new InvalidRequestException();
        }
        return await UserRepository.CreateUser(user) ?? throw new UserCreationException();
    }


    public async Task<User?> ReadUser(long userId)
    {
        if (userId <= 0)
        {
            throw new InvalidRequestException();
        }
        return await UserRepository.ReadUser(userId) ?? throw new RecordNotFoundException();
    }


    public Task<IEnumerable<User>> ReadPagedUsers(int page, int size)
    {
        throw new NotImplementedException();
    }


    public Task<User> UpdateUser(long userId, User user)
    {
        throw new NotImplementedException();
    }


    public Task<bool> DeleteUser(long userId)
    {
        throw new NotImplementedException();
    }
}
