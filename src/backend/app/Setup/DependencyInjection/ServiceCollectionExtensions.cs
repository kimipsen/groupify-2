using System.Reflection;
using app.Features.Organizations;
using app.Features.People;
using app.Setup.Dispatcher;
using app.Setup.Handlers;

namespace app.Setup.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IDispatcher, Dispatcher.Dispatcher>();

        var assembly = Assembly.GetExecutingAssembly();

        var handlerTypes = assembly
            .GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .SelectMany(t =>
                t.GetInterfaces()
                    .Where(i => i.IsGenericType &&
                               (i.GetGenericTypeDefinition() == typeof(ICommandHandler<>) ||
                               i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>) ||
                                i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)))
                    .Select(i => new { Interface = i, Implementation = t })
            );

        foreach (var handler in handlerTypes)
        {
            services.AddScoped(handler.Interface, handler.Implementation);
        }

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        return services;
    }
}
