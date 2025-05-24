using Networkie.Entities;

namespace Networkie.Persistence.Abstractions.Repositories;

public interface IStateRepository : IRepository<State>
{
    Task<IEnumerable<State>> GetAllByCountryIdAsync(Guid countryId, CancellationToken cancellationToken = default);
    Task<State?> GetByCountryIdAndNameAsync(Guid countryId, string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<State>> GetByContainsNameAsync(string name, CancellationToken cancellationToken = default);
}