using Microsoft.Extensions.DependencyInjection;
using Networkie.Persistence.Contexts;
using Networkie.Persistence.EntityFrameworkCore.Contexts;
using Networkie.Persistence.EntityFrameworkCore.IoC;

namespace Networkie.Persistence.IoC;

public static class DependencyRegistrar
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        /*services.AddDbContext<IEfCoreDbContext, NetworkieDbContext>(opts =>
            opts.UseInMemoryDatabase("denemedb")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));*/
        services.AddPersistenceEntityFrameworkCore();

        return services;
    }
}