using Micromarin.Domain.Interfaces.General;

namespace Micromarin.Domain.Interfaces;

public interface IUnitOfWork<TRepository> : IDisposable where TRepository : IRepository
{
    TRepository Repository { get; }
    int Complete();
    Task<int> CompleteAsync();
    void Dispose();
}
