using Autofac.Builder;
using Autofac.Core.Resolving.Pipeline;
using Autofac.Core;
using Castle.DynamicProxy;
using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;


namespace Micromarin.Domain.AOP.Extensions;

public static class RegistrationExtensions
{
    private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();

    public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> EnableCustomInterceptors<TLimit, TActivatorData, TRegistrationStyle>(
        this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registration)
    {
        if (registration == null)
        {
            throw new ArgumentNullException(nameof(registration));
        }

        registration.ConfigurePipeline(p =>
        {
            p.Use(PipelinePhase.Activation, MiddlewareInsertionMode.StartOfPhase, (ctx, next) =>
            {
                var options = ctx.Resolve<ProxyGenerationOptions>();
                next(ctx);

                if (ctx.Instance.GetType().IsClass && !ctx.Instance.GetType().IsAbstract && ctx.Instance.GetType().GetConstructors().Any(c => c.GetParameters().Length == 0))
                {
                    EnableClassInterception(ctx, options);
                }
                else
                {
                    EnableInterfaceInterception(ctx, options);
                }
            });
        });
        return registration;
    }

    private static void EnableClassInterception(ResolveRequestContext ctx, ProxyGenerationOptions options)
    {
        var interceptors = GetInterceptorServices(ctx.Registration, ctx.Instance.GetType()).Select(s => (IInterceptor)ctx.ResolveService(s)).ToArray();
        ctx.Instance = ProxyGenerator.CreateClassProxyWithTarget(ctx.Instance.GetType(), ctx.Instance, options, interceptors);
    }

    private static void EnableInterfaceInterception(ResolveRequestContext ctx, ProxyGenerationOptions options)
    {
        var interfaces = ctx.Instance.GetType().GetInterfaces().Where(ProxyUtil.IsAccessible).ToArray();
        if (interfaces.Any())
        {
            var interfaceToProxy = interfaces.First();
            var additionalInterfacesToProxy = interfaces.Skip(1).ToArray();
            var interceptors = GetInterceptorServices(ctx.Registration, ctx.Instance.GetType()).Select(s => (IInterceptor)ctx.ResolveService(s)).ToArray();
            ctx.Instance = ProxyGenerator.CreateInterfaceProxyWithTarget(interfaceToProxy, additionalInterfacesToProxy, ctx.Instance, options, interceptors);
        }
    }

    private static IEnumerable<Service> GetInterceptorServices(IComponentRegistration registration, Type implType)
    {
        IEnumerable<Service> first = Enumerable.Empty<Service>();
        if (registration.Metadata.TryGetValue("Autofac.Extras.DynamicProxy.RegistrationExtensions.InterceptorsPropertyName", out var value) && value is IEnumerable<Service> interceptors)
        {
            first = first.Concat(interceptors);
        }

        if (registration.Metadata.TryGetValue("Autofac.Extras.DynamicProxy.RegistrationExtensions.AttributeInterceptorsPropertyName", out value) && value is IEnumerable<Service> attributeInterceptors)
        {
            first = first.Concat(attributeInterceptors);
        }
        else
        {
            first = first.Concat(GetInterceptorServicesFromAttributes(implType));
        }

        return first;
    }

    private static IEnumerable<Service> GetInterceptorServicesFromAttributes(Type implType)
    {
        var typeInfo = implType.GetTypeInfo();
        if (!typeInfo.IsClass) return Enumerable.Empty<Service>();

        var interceptors = typeInfo.GetCustomAttributes<InterceptAttribute>(true).Select(att => att.InterceptorService);
        var interfaceInterceptors = implType.GetInterfaces().SelectMany(i => i.GetTypeInfo().GetCustomAttributes<InterceptAttribute>(true)).Select(att => att.InterceptorService);

        return interceptors.Concat(interfaceInterceptors);
    }
}