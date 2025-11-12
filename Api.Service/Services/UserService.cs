using Api.Repository.Entities;
using Api.Repository.Interfaces;
using Api.Service.Dtos;
using Api.Service.Exceptions;
using Api.Service.Interfaces;


namespace Api.Service.Services;


public class UserService(IRepository<User> userRepository) : IUserService
{
    private IRepository<User> UserRepository { get; init; } = userRepository;


    public async Task<UserResponse> CreateUser(User user)
    {
        if (user == null)
        {
            throw new InvalidRequestException();
        }

        var createdUser = await UserRepository.Create(user)
            ?? throw new UserCreationException();

        return new UserResponse(createdUser);
    }


    public async Task<UserResponse?> FindUser(long userId)
    {
        if (userId <= 0)
        {
            throw new InvalidRequestException();
        }

        var user = await UserRepository.Find(userId)
            ?? throw new RecordNotFoundException();

        return new UserResponse(user);
    }


    public PagedResponse<UserResponse> ReadPagedUsers(int page, int size)
    {
        if (page <= 0 || size <= 0)
        {
            throw new InvalidRequestException();
        }

        var entities = UserRepository.Read(page, size);
        var dtos = entities.ConvertAll(user => new UserResponse(user));

        return new PagedResponse<UserResponse>(dtos, page, size, 0);
    }


    public Task<UserResponse> UpdateUser(long userId, User user)
    {
        throw new NotImplementedException();
    }


    public Task<bool> DeleteUser(long userId)
    {
        throw new NotImplementedException();
    }
}
