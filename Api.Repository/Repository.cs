using NHibernate;
using NHibernate.Criterion;
using System.Linq.Dynamic.Core;
using Api.Repository.Interfaces;


namespace Api.Repository;


public class Repository<T>(ISessionFactory sessionFactory) : IRepository<T> where T : class
{
    public ISessionFactory SessionFactory { get; init; } = sessionFactory;


    public async Task<T?> Create(T entity)
    {
        using var session = SessionFactory.OpenSession();
        using var transaction = session.BeginTransaction();

        try
        {
            var result = await session.SaveAsync(entity);
            long id = Convert.ToInt64(result);

            transaction.Commit();

            return FindBy("Id", id);
        }
        catch (Exception)
        {
            transaction.Rollback();
            return null;
        }
    }


    public T? FindBy(string column, object value)
    {
        using var session = SessionFactory.OpenSession();

        try
        {
            string query = $"{column} == @0";
            return session.Query<T>().Where(query, value).First<T>();
        }
        catch (Exception)
        {
            return null;
        }
    }


    public List<T> Read(int page, int size)
    {
        using var session = SessionFactory.OpenSession();

        try
        {
            var result = session.QueryOver<T>()
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
        using var session = SessionFactory.OpenSession();

        try
        {
            var count = session.QueryOver<T>()
                .Select(Projections.RowCount())
                .FutureValue<int>();

            return count.Value;
        }
        catch (Exception)
        {
            return 0;
        }
    }


    public T? Update(long id, T entity)
    {
        using var session = SessionFactory.OpenSession();
        using var transaction = session.BeginTransaction();

        try
        {
            session.Update(entity, id);
            transaction.Commit();
            
            return FindBy("Id", id);
        }
        catch (Exception)
        {
            transaction.Rollback();
            return null;
        }
    }


    public Task<bool> Delete(long id)
    {
        throw new NotImplementedException();
    }
}
