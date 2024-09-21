using Micromarin.Application.Entities;
using Micromarin.Application.Interfaces.Repositories;
using Micromarin.Domain.Repositories;

namespace Micromarin.Infrastructure.Persistence.Repositories;

public class ProductRepository : EntityFrameworkRepository<Product>, IProductRepository
{
    public ProductRepository(MicromarinDbContext context) : base(context)
    {
    }
}
