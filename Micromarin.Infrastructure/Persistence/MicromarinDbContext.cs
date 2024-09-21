using Micromarin.Application.Entities;
using Micromarin.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Micromarin.Infrastructure.Persistence;

public class MicromarinDbContext : DbContext
{
    public MicromarinDbContext(DbContextOptions<MicromarinDbContext> options) : base(options)
    {
    }


    public DbSet<Order> Customers => Set<Order>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Product> Products => Set<Product>();




    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var datas = ChangeTracker.Entries<BaseEntity>();

        foreach (var data in datas)
        {
            _ = data.State switch
            {
                EntityState.Added => data.Entity.CreatedDate = DateTime.Now,
                EntityState.Modified => data.Entity.UpdatedDate = DateTime.Now,
                _ => DateTime.Now
            };
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
