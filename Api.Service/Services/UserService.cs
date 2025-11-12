using Api.Repository.Entities;
using Api.Repository.Interfaces;
using Api.Service.Dtos;
using Api.Service.Exceptions;
using Api.Service.Interfaces;


namespace Api.Service.Services;


public class UserService(IRepository<User> userRepository) : IUserService
{
    private IRepository<User> UserRepository { get; init; } = userRepository;


    public async Task<UserResultDto> CreateUser(UserCreateDto userDto)
    {
        if (userDto == null)
        {
            throw new InvalidRequestException();
        }

        var user = new User
        {
            Name = userDto.Name,
            Surname = userDto.Surname,
            Email = userDto.Email,
            Password = userDto.Password,
            PhoneNumber = userDto.PhoneNumber,
            BirthDate = userDto.BirthDate.ToDateTime(new TimeOnly(0, 0, 0))
        };
        
        var createdUser = await UserRepository.Create(user)
            ?? throw new UserCreationException();

        return new UserResultDto(createdUser);
    }


    public async Task<UserResultDto?> FindUser(long userId)
    {
        if (userId <= 0)
        {
            throw new InvalidRequestException();
        }

        var user = await UserRepository.Find(userId)
            ?? throw new RecordNotFoundException();

        return new UserResultDto(user);
    }


    public PagedResponse<UserResultDto> ReadPagedUsers(int page, int size)
    {
        if (page <= 0 || size <= 0)
        {
            throw new InvalidRequestException();
        }

        var total = UserRepository.Count();
        var entities = UserRepository.Read(page, size);
        var dtos = entities.ConvertAll(user => new UserResultDto(user));

        return new PagedResponse<UserResultDto>(dtos, page, size, total);
    }


    public Task<UserResultDto> UpdateUser(long userId, User user)
    {
        throw new NotImplementedException();
    }


    public Task<bool> DeleteUser(long userId)
    {
        throw new NotImplementedException();
    }
}
