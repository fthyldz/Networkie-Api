using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Networkie.Application.Abstractions.Common;
using Networkie.Application.Common.Cqrs.Behaviors;

namespace Networkie.Application.IoC;

public static class DependencyRegistrar
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        
        var applicationServiceType = typeof(IApplicationService);

        var serviceTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t =>
            applicationServiceType.IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false });

        foreach (var type in serviceTypes)
        {
            var interfaceType = type.GetInterfaces().FirstOrDefault(i => i != applicationServiceType);
            if (interfaceType == null) continue;
            services.AddScoped(interfaceType, type);
            Console.WriteLine($"[IoC] {interfaceType.Name} -> {type.Name} olarak eklendi.");
        }

        return services;
    }
}