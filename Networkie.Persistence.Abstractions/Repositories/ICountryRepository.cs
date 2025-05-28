using Networkie.Entities;

namespace Networkie.Persistence.Abstractions.Repositories;

public interface ICountryRepository : IRepository<Country>
{
    Task<Country?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<Country>> GetByContainsNameAsync(string name, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Country>> GetCountriesAsPagedForAdmin(int pageIndex = 0, int pageSize = 25,
        string? search = null, CancellationToken cancellationToken = default);

    Task<long> GetCountriesAsPagedTotalCountForAdmin(string? search = null,
        CancellationToken cancellationToken = default);
}