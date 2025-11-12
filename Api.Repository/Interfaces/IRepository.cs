namespace Api.Repository.Interfaces;


public interface IRepository<T> where T: class
{
    public Task<T?> Create(T entity);

    public Task<T?> FindBy(string column, object value);

    public List<T> Read(int page, int size);

    public int Count();

    public Task<T?> Update(long id, T entity);

    public Task<bool> Delete(T entity);
}
