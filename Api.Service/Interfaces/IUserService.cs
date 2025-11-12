using Api.Repository.Entities;
using Api.Service.Dtos;


namespace Api.Service.Interfaces;


public interface IUserService
{
    public Task<UserResponse> CreateUser(User user);


    public Task<UserResponse?> FindUser(long userId);


    public PagedResponse<UserResponse> ReadPagedUsers(int page, int size);


    public Task<UserResponse> UpdateUser(long userId, User user);


    public Task<bool> DeleteUser(long userId);
}
