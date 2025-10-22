
using System.Linq.Expressions;

namespace Application.Interfaces;
public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task<int> SaveAsync();

}