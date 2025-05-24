using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Networkie.Entities;

namespace Networkie.Persistence.EntityFrameworkCore.Contexts;

public interface IEfCoreDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<UserRole> UserRoles { get; set; }
    DbSet<UserEmailVerification> UserEmailVerifications { get; set; }
    DbSet<UserToken> UserTokens { get; set; }
    DbSet<UserUniversity> UserUniversities { get; set; }
    DbSet<UserSocialPlatform> UserSocialPlatforms { get; set; }
    DbSet<University> Universities { get; set; }
    DbSet<State> States { get; set; }
    DbSet<SocialPlatform> SocialPlatforms { get; set; }
    DbSet<Profession> Professions { get; set; }
    DbSet<District> Districts { get; set; }
    DbSet<Department> Departments { get; set; }
    DbSet<City> Cities { get; set; }
    DbSet<Country> Countries { get; set; }
    
    DatabaseFacade Database { get; }
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}