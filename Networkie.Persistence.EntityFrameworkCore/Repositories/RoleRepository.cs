using Microsoft.EntityFrameworkCore;
using Networkie.Entities;
using Networkie.Persistence.Abstractions.Repositories;
using Networkie.Persistence.EntityFrameworkCore.Contexts;

namespace Networkie.Persistence.EntityFrameworkCore.Repositories;

public class RoleRepository(IEfCoreDbContext context) : Repository<Role>(context), IRoleRepository
{
    public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        => await TableAsNoTracking.FirstOrDefaultAsync(u => u.Name.Equals(name), cancellationToken);
}