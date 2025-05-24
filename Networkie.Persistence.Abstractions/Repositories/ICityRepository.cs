using Networkie.Entities;

namespace Networkie.Persistence.Abstractions.Repositories;

public interface ICityRepository : IRepository<City>
{
    Task<IEnumerable<City>> GetAllByCountryIdOrStateIdAsync(Guid countryId, Guid? stateId,
        CancellationToken cancellationToken = default);

    Task<City?> GetByCountryIdOrStateIdAndNameAsync(Guid countryId, Guid? stateId, string name,
        CancellationToken cancellationToken = default);
    
    Task<IEnumerable<City>> GetByContainsNameAsync(string name, CancellationToken cancellationToken = default);
}