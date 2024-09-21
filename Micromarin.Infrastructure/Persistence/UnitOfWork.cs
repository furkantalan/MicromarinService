using Micromarin.Domain.Interfaces;
using Micromarin.Domain.Interfaces.General;


namespace Micromarin.Infrastructure.Persistence;

public class UnitOfWork<TRepository> : IUnitOfWork<TRepository> where TRepository : IRepository
{
    private readonly MicromarinDbContext _context;
    public TRepository Repository { get; }

    public UnitOfWork(MicromarinDbContext context, TRepository repository)
    {
        _context = context;
        Repository = repository;
    }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}