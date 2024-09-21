using Micromarin.Application.Entities;
using Micromarin.Domain.Interfaces.General;


namespace Micromarin.Application.Interfaces.Repositories;

public interface ICustomerRepository : IEntityFrameworkRepository<Customer>
{
}