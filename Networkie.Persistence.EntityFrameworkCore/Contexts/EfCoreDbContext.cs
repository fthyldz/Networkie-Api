using Microsoft.EntityFrameworkCore;
using Networkie.Entities;

namespace Networkie.Persistence.EntityFrameworkCore.Contexts;

public class EfCoreDbContext<TContext>(DbContextOptions<TContext> options) : DbContext(options), IEfCoreDbContext where TContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserEmailVerification> UserEmailVerifications { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
    public DbSet<UserUniversity> UserUniversities { get; set; }
    public DbSet<UserSocialPlatform> UserSocialPlatforms { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<SocialPlatform> SocialPlatforms { get; set; }
    public DbSet<Profession> Professions { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Country> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TContext).Assembly);
    }
}