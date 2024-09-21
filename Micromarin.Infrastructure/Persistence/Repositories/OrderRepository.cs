using Micromarin.Application.Entities;
using Micromarin.Application.Interfaces.Repositories;
using Micromarin.Domain.Repositories;

namespace Micromarin.Infrastructure.Persistence.Repositories;

public class OrderRepository : EntityFrameworkRepository<Order>, IOrderRepository
{
    public OrderRepository(MicromarinDbContext context) : base(context)
    {
    }
}
