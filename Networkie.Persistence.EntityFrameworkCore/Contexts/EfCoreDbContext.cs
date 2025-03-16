using Microsoft.EntityFrameworkCore;

namespace Networkie.Persistence.EntityFrameworkCore.Contexts;

public class EfCoreDbContext<TContext>(DbContextOptions<TContext> options) : DbContext(options), IEfCoreDbContext where TContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TContext).Assembly);
    }
}