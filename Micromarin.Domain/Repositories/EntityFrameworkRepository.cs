using Micromarin.Domain.Helpers;
using Micromarin.Domain.Interfaces.General;
using Micromarin.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Micromarin.Domain.Repositories;

public class EntityFrameworkRepository<T> : IEntityFrameworkRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public EntityFrameworkRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
    }

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Update(entity);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbSet.Where(filter).ToListAsync();
    }

    public async Task<ListResult<T>> GetPagedAsync(ListByFilterRequest request)
    {
        return await EfCoreHelper.GetPagedResultAsync(_dbSet, request);
    }

    public async Task RemoveAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }
}