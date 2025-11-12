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

        var userWithSameEmail = UserRepository.FindBy("Email", userDto.Email);
        if (userWithSameEmail != null)
        {
            throw new UserEmailAlreadyExistsException(userDto.Email);
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
        
        var createdUser = await UserRepository.Create(user) ?? throw new RecordCreationException();
        return new UserResultDto(createdUser);
    }


    public UserResultDto? FindUser(long userId)
    {
        if (userId <= 0)
        {
            throw new InvalidRequestException();
        }

        var user = UserRepository.FindBy("Id", userId) ?? throw new RecordNotFoundException();
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

        return new PagedResponse<UserResultDto>(dtos, total, page, size);
    }


    public UserResultDto UpdateUser(long userId, UserUpdateDto userDto)
    {
        if (userId <= 0 || userDto == null)
        {
            throw new InvalidRequestException();
        }

        var user = UserRepository.FindBy("Id", userId)
            ?? throw new RecordNotFoundException();

        user.Name = userDto.Name ?? user.Name;
        user.Surname = userDto.Surname ?? user.Surname;
        user.PhoneNumber = userDto.PhoneNumber ?? user.PhoneNumber;
        user.BirthDate = userDto.BirthDate?.ToDateTime(new TimeOnly(0, 0, 0, 0)) ?? user.BirthDate;

        var updatedUser = UserRepository.Update(userId, user)
            ?? throw new RecordUpdateException();

        return new UserResultDto(updatedUser);
    }


    public async Task DeleteUser(long userId)
    {
        if (userId <= 0)
        {
            throw new InvalidRequestException();
        }

        await UserRepository.Delete(userId);
    }
}
