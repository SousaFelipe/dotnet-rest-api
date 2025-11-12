using Api.Data.Entities;
using Api.Data.Interfaces;


namespace Api.Data.Repositories;


public class UserRepository : IUserRepository
{
    public async Task<User?> CreateUser(User user)
    {
        using var session = NHibernateHelper.OpenSession();
        using var transaction = session.BeginTransaction();

        try
        {
            object savedUserId = await session.SaveAsync(user);
            long userId = Convert.ToInt64(savedUserId);

            transaction.Commit();

            return await ReadUser(userId);
        }
        catch (Exception)
        {
            transaction.Rollback();
            session.Close();
            return null;
        }
    }


    public async Task<User?> ReadUser(long userId)
    {
        using var session = NHibernateHelper.OpenSession();

        try
        {
            return await session.GetAsync<User?>(userId);
        }
        catch (Exception)
        {
            session.Close();
            return null;
        }
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