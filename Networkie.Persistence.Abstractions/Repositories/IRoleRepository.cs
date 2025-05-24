using Networkie.Entities;

namespace Networkie.Persistence.Abstractions.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}