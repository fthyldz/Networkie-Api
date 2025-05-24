using Networkie.Entities;

namespace Networkie.Persistence.Abstractions.Repositories;

public interface IUniversityRepository : IRepository<University>
{
    Task<University?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<University>> GetByContainsNameAsync(string name, CancellationToken cancellationToken = default);
}