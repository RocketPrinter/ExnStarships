using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExnStarships.Data;

public class Repository<T> : IRepository<T>
    where T : class
{
    MainContext context;
    DbSet<T> dbset;

    public Repository(MainContext context)
    {
        this.context = context;
        this.dbset = context.Set<T>();
    }

    public void Add(T entity) => dbset.Add(entity);

    public void Delete(T entity) => dbset.Remove(entity);

    public T? GetById(int id) => dbset.Find(id);

    public IQueryable<T> Query(Expression<Func<T, bool>> expression)
        => dbset.Where(expression);

    public void Update(T entity)
    {
        dbset.Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
        context.SaveChanges();
    }

    public List<T> GetAll()
    {
        return dbset.ToList();
    }
}
