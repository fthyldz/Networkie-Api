using Carter;
using Networkie.Api.ExceptionHandlers;

namespace Networkie.Api.IoC;

public static class DependencyRegistrar
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddCors(opts =>
        {
            opts.AddPolicy("DevelopmentCorsPolicy", policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
            });
    
            opts.AddPolicy("ProductionCorsPolicy", policy =>
            {
                policy.WithOrigins("https://*.com", "http://*.com");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });
        });
        
        services.AddCarter();
        
        services.AddExceptionHandler<UnauthorizedExceptionHandler>();
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        
        return services;
    }
}