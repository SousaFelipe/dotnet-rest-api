using Api.Service.Dtos;


namespace Api.Service.Interfaces;


public interface IUserService
{
    public Task<UserResult> CreateUser(UserRequestCreate userDto);


    public Task<UserResult?> FindUser(string column, object value);


    public PagedResponse<UserResult> ReadPagedUsers(int page, int size);


    public Task<UserResult> UpdateUser(long userId, UserRequestUpdate userDto);


    public Task DeleteUser(long userId);
}
