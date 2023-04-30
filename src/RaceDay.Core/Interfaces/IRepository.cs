using System.Linq.Expressions;

namespace RaceDay.Core.Interfaces;

public interface IRepository<T> where T : class, IEntity
{
    public void Create(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public Task<T?> GetById(Guid guid);
    public Task<IEnumerable<T>> GetAll();
    public Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> predicate);
}