using Networkie.Entities;

namespace Networkie.Persistence.Abstractions.Repositories;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<Department?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Department>> GetByContainsNameAsync(string name, CancellationToken cancellationToken = default);
}