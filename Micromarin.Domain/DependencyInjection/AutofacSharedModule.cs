using Autofac;
using AutoMapper;
using Castle.DynamicProxy;
using FluentValidation;
using MediatR;
using Micromarin.Domain.AOP.Extensions;
using Micromarin.Domain.Interfaces.General;
using Micromarin.Domain.Mappings.Converter;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using MongoDB.Bson;


namespace Micromarin.Domain.DependencyInjection;

public class AutofacSharedModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.FullName.Contains("Application"))
            .ToArray();

        // Localization options
        builder.Register(c =>
        {
            var options = Options.Create(new LocalizationOptions
            {
                ResourcesPath = "Resources"
            });

            return options;
        }).As<IOptions<LocalizationOptions>>().SingleInstance();

        builder.RegisterType<ResourceManagerStringLocalizerFactory>()
            .As<IStringLocalizerFactory>()
            .SingleInstance();

        //builder.Register(c =>
        //{
        //    var context = c.Resolve<IComponentContext>();
        //    return new ProxyGenerationOptions
        //    {
        //        Selector = context.Resolve<IInterceptorSelector>()
        //    };
        //}).As<ProxyGenerationOptions>().SingleInstance();

        //builder.RegisterAssemblyTypes(assemblies)
        //    .AsClosedTypesOf(typeof(IRequestHandler<,>))
        //    .AsImplementedInterfaces();

        //builder.RegisterAssemblyTypes(assemblies)
        //    .AsClosedTypesOf(typeof(IValidator<>))
        //    .AsImplementedInterfaces();


        //builder.RegisterAssemblyTypes(assemblies)
        //        .AsImplementedInterfaces()
        //        .EnableCustomInterceptors()
        //        .InstancePerDependency();

        //builder.Register(context => new MapperConfiguration(cfg =>
        //{
        //    cfg.CreateMap<string, ObjectId>().ConvertUsing<StringToObjectIdConverter>();
        //    cfg.AddMaps(assemblies);
        //}).CreateMapper()).As<IMapper>().InstancePerLifetimeScope();

    }
}