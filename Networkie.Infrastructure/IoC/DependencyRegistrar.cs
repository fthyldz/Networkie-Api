using Microsoft.Extensions.DependencyInjection;
using Networkie.Application.Abstractions.Providers;
using Networkie.Infrastructure.Providers;

namespace Networkie.Infrastructure.IoC;

public static class DependencyRegistrar
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITokenProvider, TokenProvider>();

        return services;
    }
}