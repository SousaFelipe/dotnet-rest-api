using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using System.Linq.Dynamic.Core;
using Api.Repository.Interfaces;


namespace Api.Repository;


public class Repository<T>(ISession session) : IRepository<T> where T : class
{
    public ISession Session { get; init; } = session;


    public async Task<T?> Create(T entity)
    {
        using var transaction = Session.BeginTransaction();

        try
        {
            var result = await Session.SaveAsync(entity);
            long id = Convert.ToInt64(result);

            transaction.Commit();

            return await FindBy("Id", id);
        }
        catch (Exception)
        {
            transaction.Rollback();
            return null;
        }
    }


    public async Task<T?> FindBy(string column, object value)
    {
        try
        {
            string query = $"{column} == @0";
            return await Session.Query<T>().Where(query, value).FirstAsync();
        }
        catch (Exception)
        {
            return null;
        }
    }


    public List<T> Read(int page, int size)
    {
        try
        {
            var result = Session.QueryOver<T>()
                .Skip((page - 1) * size)
                .Take(size)
                .Future<T>();

            return [.. result];
        }
        catch (Exception)
        {
            return [];
        }
    }


    public int Count()
    {
        try
        {
            var count = Session.QueryOver<T>()
                .Select(Projections.RowCount())
                .FutureValue<int>();

            return count.Value;
        }
        catch (Exception)
        {
            return 0;
        }
    }


    public async Task<T?> Update(long id, T entity)
    {
        using var transaction = Session.BeginTransaction();

        try
        {
            await Session.UpdateAsync(entity, id);
            transaction.Commit();
            
            return await FindBy("Id", id);
        }
        catch (Exception)
        {
            transaction.Rollback();
            return null;
        }
    }


    public async Task<bool> Delete(T entity)
    {
        using var transaction = Session.BeginTransaction();

        try
        {
            await Session.DeleteAsync(entity);
            transaction.Commit();

            return true;
        }
        catch (Exception)
        {
            transaction.Rollback();
            return false;
        }
    }
}
