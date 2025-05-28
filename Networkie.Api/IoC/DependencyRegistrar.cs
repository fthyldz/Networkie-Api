using System.Security.Claims;
using System.Text;
using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Networkie.Api.ExceptionHandlers;
using Networkie.Application.Common.Exceptions;

namespace Networkie.Api.IoC;

public static class DependencyRegistrar
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
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
                policy.AllowCredentials();
                policy.WithOrigins("https://networkie.fthyldz.site", "http://localhost:4200");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
            JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWT:Secret")!)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    RoleClaimType = ClaimTypes.Role
                };

                /*options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = _ => throw new UnauthorizedException("Unauthorized access"),
                    OnChallenge = _ => throw new UnauthorizedException("Unauthorized access")
                };*/
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
        });
        
        services.AddCarter();
        
        services.AddExceptionHandler<UnauthorizedExceptionHandler>();
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services.AddMemoryCache();
        
        return services;
    }
}