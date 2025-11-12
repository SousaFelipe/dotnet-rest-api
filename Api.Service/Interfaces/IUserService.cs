using Api.Repository.Entities;
using Api.Service.Dtos;


namespace Api.Service.Interfaces;


public interface IUserService
{
    public Task<UserResultDto> CreateUser(UserCreateDto userDto);


    public Task<UserResultDto?> FindUser(long userId);


    public PagedResponse<UserResultDto> ReadPagedUsers(int page, int size);


    public Task<UserResultDto> UpdateUser(long userId, User user);


    public Task<bool> DeleteUser(long userId);
}
