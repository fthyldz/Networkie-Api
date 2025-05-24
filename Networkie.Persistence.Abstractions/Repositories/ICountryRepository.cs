using Networkie.Entities;

namespace Networkie.Persistence.Abstractions.Repositories;

public interface ICountryRepository : IRepository<Country>
{
    Task<Country?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<Country>> GetByContainsNameAsync(string name, CancellationToken cancellationToken = default);
}