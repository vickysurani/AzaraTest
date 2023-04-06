namespace azara.repository;

public interface IRepository<AppDbContext, T> where T : BaseEntity where AppDbContext : DbContext
{
    IQueryable<T> Retrieve();

    T Retrieve(Guid id);

    IQueryable<T> Retrieve(Func<T, bool> condition);

    T Insert(T entity);

    T Update(T entity);

    bool Delete(T entity);

    IQueryable<T> Retrieve(int Index);

    IQueryable<T> Retrieve(Func<T, bool> condition, int Index);

    void Delete(Guid id);

    void Delete(Func<T, bool> condition);
}