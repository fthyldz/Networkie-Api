using Microsoft.EntityFrameworkCore;
using Networkie.Entities;
using Networkie.Persistence.Abstractions.Repositories;
using Networkie.Persistence.EntityFrameworkCore.Contexts;

namespace Networkie.Persistence.EntityFrameworkCore.Repositories;

public class CityRepository(IEfCoreDbContext context) : Repository<City>(context), ICityRepository
{
    public async Task<IEnumerable<City>> GetAllByCountryIdOrStateIdAsync(Guid countryId, Guid? stateId,
        CancellationToken cancellationToken = default)
    {
        var query = TableAsNoTracking.Where(c => c.CountryId == countryId);
        if (stateId.HasValue)
            query = query.Where(c => c.StateId == stateId.Value);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<City?> GetByCountryIdOrStateIdAndNameAsync(Guid countryId, Guid? stateId, string name,
        CancellationToken cancellationToken = default)
    {
        var query = TableAsNoTracking.Where(c => c.CountryId == countryId);
        if (stateId.HasValue)
            query = query.Where(c => c.StateId == stateId.Value);
        return await query.FirstOrDefaultAsync(c => c.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<City>> GetByContainsNameAsync(string name,
        CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.Where(c => c.Name.ToLower().Contains(name.ToLower())).ToListAsync(cancellationToken);
}