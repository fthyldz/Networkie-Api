using Microsoft.EntityFrameworkCore;
using Networkie.Entities;
using Networkie.Persistence.Abstractions.Repositories;
using Networkie.Persistence.EntityFrameworkCore.Contexts;

namespace Networkie.Persistence.EntityFrameworkCore.Repositories;

public class StateRepository(IEfCoreDbContext context) : Repository<State>(context), IStateRepository
{
    public async Task<IEnumerable<State>> GetAllByCountryIdAsync(Guid countryId,
        CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.Where(s => s.CountryId == countryId).ToListAsync(cancellationToken);

    public async Task<State?> GetByCountryIdAndNameAsync(Guid countryId, string name,
        CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.FirstOrDefaultAsync(s => s.CountryId == countryId && s.Name == name,
            cancellationToken);

    public async Task<IEnumerable<State>> GetByContainsNameAsync(string name,
        CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.Where(c => EF.Functions.ILike(c.Name, $"%{name}%")).ToListAsync(cancellationToken);
}