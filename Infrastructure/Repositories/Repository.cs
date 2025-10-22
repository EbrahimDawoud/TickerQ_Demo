
using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Repositories;


public class Repository<TEntity>(ApplicationDbContext context) : IRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

  
    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        => await _dbSet.FirstOrDefaultAsync(predicate);

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null)
        => predicate != null ? await _dbSet.Where(predicate).ToListAsync()
                              : await _dbSet.ToListAsync();

    public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);
    public void Update(TEntity entity) => _dbSet.Update(entity);
    public void Remove(TEntity entity) => _dbSet.Remove(entity);

    public async Task<int> SaveAsync() => await context.SaveChangesAsync();
}