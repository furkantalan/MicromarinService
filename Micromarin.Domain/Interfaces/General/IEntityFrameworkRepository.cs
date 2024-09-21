using Micromarin.Domain.Models;
using System.Linq.Expressions;

namespace Micromarin.Domain.Interfaces.General;

public interface IEntityFrameworkRepository<T> : IRepository where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task RemoveAsync(Guid id);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter);
    Task<ListResult<T>> GetPagedAsync(ListByFilterRequest request);
}
