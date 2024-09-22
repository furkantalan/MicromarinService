using Autofac;
using Micromarin.Domain.Interfaces;
using Micromarin.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Micromarin.Infrastructure.DependencyInjection;

public class AutofacInfrastructureModule : Module
{
    private readonly IConfiguration _configuration;

    public AutofacInfrastructureModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {

        builder.Register(c =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<MicromarinDbContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SQLServer"));

            return new MicromarinDbContext(optionsBuilder.Options);
        }).AsSelf().InstancePerLifetimeScope();


        // Repository registrations
        var assemblies = typeof(AutofacInfrastructureModule).Assembly;
        builder.RegisterAssemblyTypes(assemblies)
               .Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

        builder.RegisterGeneric(typeof(UnitOfWork<>))
                .As(typeof(IUnitOfWork<>))
                .InstancePerLifetimeScope();
    }
}