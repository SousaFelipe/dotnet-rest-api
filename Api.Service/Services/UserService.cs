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

        var userWithSameEmail = await UserRepository.FindBy("Email", userDto.Email);
        if (userWithSameEmail != null)
        {
            throw new UserEmailAlreadyExistsException(userDto.Email);
        }

        var user = new User
        {
            Name = userDto.Name,
            Surname = userDto.Surname,
            Email = userDto.Email,
            PhoneNumber = userDto.PhoneNumber,
            BirthDate = userDto.BirthDate.ToDateTime(new TimeOnly(0, 0, 0))
        };
        user.SetPassword(userDto.Password);
        
        var createdUser = await UserRepository.Create(user) ?? throw new RecordCreateException();
        return new UserResultDto(createdUser);
    }


    public async Task<UserResultDto?> FindUser(string column, object value)
    {
        if (value == null)
        {
            throw new InvalidRequestException();
        }

        var foundUser = await UserRepository.FindBy(column, value)
            ?? throw new RecordNotFoundException();

        return new UserResultDto(foundUser);
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


    public async Task<UserResultDto> UpdateUser(long userId, UserUpdateDto userDto)
    {
        if (userId <= 0 || userDto == null)
        {
            throw new InvalidRequestException();
        }

        var user = await UserRepository.FindBy("Id", userId)
            ?? throw new RecordNotFoundException();

        user.Name = userDto.Name ?? user.Name;
        user.Surname = userDto.Surname ?? user.Surname;
        user.PhoneNumber = userDto.PhoneNumber ?? user.PhoneNumber;
        user.BirthDate = userDto.BirthDate?.ToDateTime(new TimeOnly(0, 0, 0, 0)) ?? user.BirthDate;

        var updatedUser = await UserRepository.Update(userId, user)
            ?? throw new RecordUpdateException();

        return new UserResultDto(updatedUser);
    }


    public async Task DeleteUser(long userId)
    {
        if (userId <= 0)
        {
            throw new InvalidRequestException();
        }

        var userFound = await UserRepository.FindBy("Id", userId)
            ?? throw new RecordNotFoundException();

        var userHasBeenRemoved = await UserRepository.Delete(userFound);
        if (!userHasBeenRemoved)
        {
            throw new RecordDeleteException();
        }
    }
}
