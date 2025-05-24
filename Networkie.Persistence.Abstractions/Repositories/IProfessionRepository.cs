using Networkie.Entities;

namespace Networkie.Persistence.Abstractions.Repositories;

public interface IProfessionRepository : IRepository<Profession>
{
    Task<Profession?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<Profession>> GetByContainsNameAsync(string name, CancellationToken cancellationToken = default);
}