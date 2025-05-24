using Microsoft.Extensions.DependencyInjection;
using Networkie.Persistence.Abstractions;
using Networkie.Persistence.Abstractions.Repositories;
using Networkie.Persistence.EntityFrameworkCore.Repositories;

namespace Networkie.Persistence.EntityFrameworkCore.IoC;

public static class DependencyRegistrar
{
    public static IServiceCollection AddPersistenceEntityFrameworkCore(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IUserEmailVerificationRepository, UserEmailVerificationRepository>();
        services.AddScoped<IUserTokenRepository, UserTokenRepository>();
        services.AddScoped<IProfessionRepository, ProfessionRepository>();
        services.AddScoped<IUniversityRepository, UniversityRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<ICountryRepository, CountrytRepository>();
        services.AddScoped<IStateRepository, StateRepository>();
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<IDistrictRepository, DistrictRepository>();
        services.AddScoped<ISocialPlatformRepository, SocialPlatformRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}