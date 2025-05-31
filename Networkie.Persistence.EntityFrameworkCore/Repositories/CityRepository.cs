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
        await TableAsNoTracking.Where(c => EF.Functions.ILike(c.Name, $"%{name}%")).ToListAsync(cancellationToken);

    public async Task<IEnumerable<City>> GetCitiesAsPagedForAdmin(int pageIndex = 0, int pageSize = 25,
        string? searchCity = null, List<Guid>? searchCountry = null, CancellationToken cancellationToken = default)
    {
        var query = TableAsNoTracking.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchCity))
            query = query.Where(c => EF.Functions.ILike(c.Name, $"%{searchCity}%"));

        if (searchCountry != null && searchCountry.Any())
            query = query.Where(u => searchCountry.Contains(u.CountryId));
        else if (searchCountry != null && !searchCountry.Any())
            return new List<City>();

        return await query
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<long> GetCitiesAsPagedTotalCountForAdmin(string? searchCity = null, List<Guid>? searchCountry = null, 
        CancellationToken cancellationToken = default)
    {
        var query = TableAsNoTracking.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchCity))
            query = query.Where(c => EF.Functions.ILike(c.Name, $"%{searchCity}%"));

        if (searchCountry != null && searchCountry.Any())
            query = query.Where(u => searchCountry.Contains(u.CountryId));

        return await query.CountAsync(cancellationToken);
    }
}