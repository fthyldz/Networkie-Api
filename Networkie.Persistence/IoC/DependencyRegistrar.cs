using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Networkie.Entities;
using Networkie.Entities.Enums;
using Networkie.Persistence.Contexts;
using Networkie.Persistence.EntityFrameworkCore.Contexts;
using Networkie.Persistence.EntityFrameworkCore.IoC;

namespace Networkie.Persistence.IoC;

public static class DependencyRegistrar
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IEfCoreDbContext, NetworkieDbContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("LocalDb")));
        /*services.AddDbContext<IEfCoreDbContext, NetworkieDbContext>(opts =>
            opts.UseInMemoryDatabase(configuration.GetConnectionString("InMemoryDb")!)
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));*/
        services.AddPersistenceEntityFrameworkCore();

        return services;
    }
    
    public static async Task MigrateDatabaseAsync(this IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<NetworkieDbContext>();
        
        await dbContext.Database.MigrateAsync();
        
        await SeedDatabaseAsync(dbContext);
    }

    private static async Task SeedDatabaseAsync(NetworkieDbContext dbContext)
    {
        var isChanged = false;
        if (!await dbContext.Roles.AnyAsync())
        {
            await dbContext.AddRangeAsync(GetRoles());
            isChanged = true;
        }
        if (!await dbContext.Professions.AnyAsync())
        {
            await dbContext.AddRangeAsync(GetProfessions());
            isChanged = true;
        }
        if (!await dbContext.Countries.AnyAsync())
        {
            await dbContext.AddRangeAsync(GetCountries());
            isChanged = true;
        }
        if (!await dbContext.States.AnyAsync())
        {
            await dbContext.AddRangeAsync(GetStates());
            isChanged = true;
        }
        if (!await dbContext.Cities.AnyAsync())
        {
            await dbContext.AddRangeAsync(GetCities());
            isChanged = true;
        }
        if (!await dbContext.Districts.AnyAsync())
        {
            await dbContext.AddRangeAsync(GetDistricts());
            isChanged = true;
        }
        if (!await dbContext.SocialPlatforms.AnyAsync())
        {
            await dbContext.AddRangeAsync(GetSocialPlatforms());
            isChanged = true;
        }
        if (!await dbContext.Universities.AnyAsync())
        {
            await dbContext.AddRangeAsync(GetUniversities());
            isChanged = true;
        }
        if (!await dbContext.Departments.AnyAsync())
        {
            await dbContext.AddRangeAsync(GetDepartments());
            isChanged = true;
        }
        if (!await dbContext.Users.AnyAsync())
        {
            await dbContext.AddRangeAsync(GetUsers());
            isChanged = true;
        }
        if (!await dbContext.UserRoles.AnyAsync())
        {
            await dbContext.AddRangeAsync(GetUserRoles());
            isChanged = true;
        }
        if (isChanged)
            await dbContext.SaveChangesAsync();
    }

    private static List<Role> GetRoles() =>
    [
        new()
        {
            Id = new Guid("80a45b88-e8fc-4d29-85da-a2e4222952ba"),
            Name = "Admin"
        },

        new()
        {
            Id = new Guid("efeece2e-3e89-4c7b-b25c-a428898904a5"),
            Name = "User"
        }
    ];

    private static List<Profession> GetProfessions() =>
    [
        new()
        {
            Name = "Akademisyen"
        }
    ];
    
    private static List<Country> GetCountries() =>
    [
        new()
        {
            Id = new Guid("600c9c04-d3bb-4db3-8dbe-c5eb681adfa8"),
            Name = "Türkiye"
        }
    ];
    
    private static List<State> GetStates() =>
    [
        new()
        {
            Id = new Guid("8c5888dc-ebde-4ac2-84d7-2b8c947e9912"),
            Name = "Trabzon",
            CountryId = new Guid("600c9c04-d3bb-4db3-8dbe-c5eb681adfa8")
        }
    ];
    
    private static List<City> GetCities() =>
    [
        new()
        {
            Id = new Guid("05120a84-c1c0-4a5f-86d0-7ee3f179033a"),
            Name = "Trabzon",
            CountryId = new Guid("600c9c04-d3bb-4db3-8dbe-c5eb681adfa8"),
            StateId = new Guid("8c5888dc-ebde-4ac2-84d7-2b8c947e9912")
        }
    ];
    
    private static List<District> GetDistricts() =>
    [
        new()
        {
            Name = "Of",
            CityId = new Guid("05120a84-c1c0-4a5f-86d0-7ee3f179033a")
        }
    ];
    
    private static List<SocialPlatform> GetSocialPlatforms() =>
    [
        new()
        {
            Name = "LinkedIn",
            IsRequired = true
        }
    ];
    
    private static List<University> GetUniversities() =>
    [
        new()
        {
            Name = "Karadeniz Teknik Üniversitesi"
        }
    ];

    private static List<Department> GetDepartments() =>
    [
        new()
        {
            Name = "Yazılım Mühendisliği"
        }
    ];
    
    private static List<User> GetUsers() =>
    [
        new()
        {
            Id = new Guid("ccf64c80-2d62-420e-be0c-e21f45b0d529"),
            FirstName = "Admin",
            Email = "admin@admin.com",
            PasswordHashed = "$2a$11$do0kpdsQTD9g.UWcQsAdae2qw8whnmt2yYfYV5wh/WJ9gQdFne/Ky",
            PasswordSalt = "$2a$11$do0kpdsQTD9g.UWcQsAdae",
            IsEmailVerified = true,
            IsProfileCompleted = true
        }
    ];
    
    private static List<UserRole> GetUserRoles() =>
    [
        new()
        {
            UserId = new Guid("ccf64c80-2d62-420e-be0c-e21f45b0d529"),
            RoleId = new Guid("80a45b88-e8fc-4d29-85da-a2e4222952ba")
        }
    ];
}