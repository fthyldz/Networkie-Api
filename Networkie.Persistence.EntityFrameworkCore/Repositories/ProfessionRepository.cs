using Microsoft.EntityFrameworkCore;
using Networkie.Entities;
using Networkie.Persistence.Abstractions.Repositories;
using Networkie.Persistence.EntityFrameworkCore.Contexts;

namespace Networkie.Persistence.EntityFrameworkCore.Repositories;

public class ProfessionRepository(IEfCoreDbContext context) : Repository<Profession>(context), IProfessionRepository
{
    public async Task<Profession?> GetByNameAsync(string name, CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower(), cancellationToken);

    public async Task<IEnumerable<Profession>> GetByContainsNameAsync(string name,
        CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToListAsync(cancellationToken);
}