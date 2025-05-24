using Microsoft.EntityFrameworkCore;
using Networkie.Entities;
using Networkie.Persistence.Abstractions.Repositories;
using Networkie.Persistence.EntityFrameworkCore.Contexts;

namespace Networkie.Persistence.EntityFrameworkCore.Repositories;

public class DepartmentRepository(IEfCoreDbContext context) : Repository<Department>(context), IDepartmentRepository
{
    public async Task<Department?> GetByNameAsync(string name, CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.FirstOrDefaultAsync(d => d.Name.ToLower() == name.ToLower(), cancellationToken);

    public async Task<IEnumerable<Department>> GetByContainsNameAsync(string name,
        CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.Where(c => c.Name.ToLower().Contains(name.ToLower())).ToListAsync(cancellationToken);
}