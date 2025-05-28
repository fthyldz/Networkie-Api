using Networkie.Entities;

namespace Networkie.Persistence.Abstractions.Repositories;

public interface IProfessionRepository : IRepository<Profession>
{
    Task<Profession?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<Profession>> GetByContainsNameAsync(string name, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Profession>> GetProfessionsAsPagedForAdmin(int pageIndex = 0, int pageSize = 25,
        string? search = null, CancellationToken cancellationToken = default);

    Task<long> GetProfessionsAsPagedTotalCountForAdmin(string? search = null,
        CancellationToken cancellationToken = default);
}