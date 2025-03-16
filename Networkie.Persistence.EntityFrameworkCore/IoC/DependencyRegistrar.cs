using Microsoft.Extensions.DependencyInjection;
using Networkie.Persistence.Abstractions;

namespace Networkie.Persistence.EntityFrameworkCore.IoC;

public static class DependencyRegistrar
{
    public static IServiceCollection AddPersistenceEntityFrameworkCore(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}