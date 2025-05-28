using Networkie.Entities;

namespace Networkie.Persistence.Abstractions.Repositories;

public interface IUniversityRepository : IRepository<University>
{
    Task<University?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<IEnumerable<University>> GetByContainsNameAsync(string name, CancellationToken cancellationToken = default);

    Task<IEnumerable<University>> GetUniversitiesAsPagedForAdmin(int pageIndex = 0, int pageSize = 25,
        string? search = null, CancellationToken cancellationToken = default);

    Task<long> GetUniversitiesAsPagedTotalCountForAdmin(string? search = null,
        CancellationToken cancellationToken = default);
}