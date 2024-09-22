using Autofac;
using AutoMapper;
using MediatR;
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

    }
}