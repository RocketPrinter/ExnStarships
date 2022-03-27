using System.Linq.Expressions;

namespace ExnStarships.Data;

public interface IRepository<T> where T : class
{
    void Add(T entity);
    void Delete(T entity);
    void Update(T entity);
    T? GetById(int id);
    IQueryable<T> Query(Expression<Func<T, bool>> expression);
    List<T> GetAll();
}
