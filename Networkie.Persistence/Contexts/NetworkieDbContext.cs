using Microsoft.EntityFrameworkCore;
using Networkie.Persistence.EntityFrameworkCore.Contexts;

namespace Networkie.Persistence.Contexts;

public class NetworkieDbContext(DbContextOptions<NetworkieDbContext> options) : EfCoreDbContext<NetworkieDbContext>(options)
{
}