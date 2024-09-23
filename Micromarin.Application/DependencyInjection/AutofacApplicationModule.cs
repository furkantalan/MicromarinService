using Autofac;
using AutoMapper;
using MediatR;

namespace Micromarin.Application.DependencyInjection;

public class AutofacApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
               .AsClosedTypesOf(typeof(IRequestHandler<,>))
               .AsImplementedInterfaces();

        builder.RegisterType<Mediator>()
               .As<IMediator>()
               .InstancePerLifetimeScope();

        builder.Register(context =>
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
            });
            return configuration.CreateMapper();
        }).As<IMapper>().InstancePerLifetimeScope();


    }
}