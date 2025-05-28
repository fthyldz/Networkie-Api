using Networkie.Entities;

namespace Networkie.Persistence.Abstractions.Repositories;

public interface ICityRepository : IRepository<City>
{
    Task<IEnumerable<City>> GetAllByCountryIdOrStateIdAsync(Guid countryId, Guid? stateId,
        CancellationToken cancellationToken = default);

    Task<City?> GetByCountryIdOrStateIdAndNameAsync(Guid countryId, Guid? stateId, string name,
        CancellationToken cancellationToken = default);
    
    Task<IEnumerable<City>> GetByContainsNameAsync(string name, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<City>> GetCitiesAsPagedForAdmin(int pageIndex = 0, int pageSize = 25,
        string? searchCity = null, List<Guid>? searchCountry = null, CancellationToken cancellationToken = default);

    Task<long> GetCitiesAsPagedTotalCountForAdmin(string? searchCity = null, List<Guid>? searchCountry = null,
        CancellationToken cancellationToken = default);
}