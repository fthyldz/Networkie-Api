using Microsoft.EntityFrameworkCore;
using Networkie.Entities;
using Networkie.Persistence.Abstractions.Repositories;
using Networkie.Persistence.EntityFrameworkCore.Contexts;

namespace Networkie.Persistence.EntityFrameworkCore.Repositories;

public class CountrytRepository(IEfCoreDbContext context) : Repository<Country>(context), ICountryRepository
{
    public async Task<Country?> GetByNameAsync(string name, CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.FirstOrDefaultAsync(c => c.Name == name, cancellationToken);

    public async Task<IEnumerable<Country>> GetByContainsNameAsync(string name,
        CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.Where(c => c.Name.ToLower().Contains(name.ToLower())).ToListAsync(cancellationToken);
}