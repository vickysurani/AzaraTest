namespace azara.repository.Implementations;

public class Repository<AppDbContext, T> : IRepository<AppDbContext, T> where AppDbContext : DbContext where T : BaseEntity
{
    private readonly AppDbContext context;
    private DbSet<T> entities;
    private int PageSize = 10;
    private string errorMessage = string.Empty;

    public Repository(AppDbContext context)
    {
        this.context = context;
        entities = context.Set<T>();
    }

    public bool Delete(T entity)
    {
        Update(entity);
        return true;
    }

    public bool DeleteAll()
    {
        foreach (var en in entities) entities.Remove(en);
        SaveChange();
        return true;
    }

    public T Insert(T entity)
    {
        if (entity == null) throw new ArgumentNullException("entity");
        entity.Modified = DateTime.UtcNow;
        entities.Add(entity);
        SaveChange();
        return entity;
    }

    public IQueryable<T> Retrieve(Func<T, bool> condition) => entities.Where(condition).AsQueryable();

    public IQueryable<T> Retrieve() => entities.AsQueryable();

    public T Retrieve(Guid id) => entities.Find(id);

    public T Update(T entity)
    {
        if (entity == null) throw new ArgumentNullException("entity");
        entity.Modified = DateTime.UtcNow;
        SaveChange();
        return entity;
    }

    public void Delete(Func<T, bool> condition)
    {
        var entity = entities.Where(condition).ToList();
        foreach (var e in entity) e.Deleted = true;
        SaveChange();
    }

    public void Delete(Guid id)
    {
        var entity = entities.Find(id);
        Delete(entity);
    }

    public IQueryable<T> Retrieve(int Index) => entities.AsQueryable().Skip((Index - 1) * PageSize).Take(PageSize);

    public IQueryable<T> Retrieve(Func<T, bool> condition, int Index)
    {
        if (condition == null) throw new ArgumentNullException("condition");
        return entities.Where(condition).AsQueryable().Skip((Index - 1) * PageSize).Take(PageSize);
    }

    private void SaveChange() => context.SaveChanges(); // Commit your data in SQL
}