using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Networkie.Application.Abstractions.Providers;
using Networkie.Application.Abstractions.Services;
using Networkie.Infrastructure.Providers;
using Networkie.Infrastructure.Services;

namespace Networkie.Infrastructure.IoC;

public static class DependencyRegistrar
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICryptoProvider, CryptoProvider>();
        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddSingleton<IEmailService>(new BrevoEmailService(configuration["Brevo:ApiKey"]!));
        
        return services;
    }
}