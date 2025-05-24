using Microsoft.EntityFrameworkCore;
using Networkie.Entities;
using Networkie.Persistence.Abstractions.Repositories;
using Networkie.Persistence.EntityFrameworkCore.Contexts;

namespace Networkie.Persistence.EntityFrameworkCore.Repositories;

public class UniversityRepository(IEfCoreDbContext context) : Repository<University>(context), IUniversityRepository
{
    public async Task<University?> GetByNameAsync(string name, CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.FirstOrDefaultAsync(u => u.Name.ToLower() == name.ToLower(), cancellationToken);

    public async Task<IEnumerable<University>> GetByContainsNameAsync(string name,
        CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.Where(u => u.Name.ToLower().Contains(name.ToLower())).ToListAsync(cancellationToken);
}