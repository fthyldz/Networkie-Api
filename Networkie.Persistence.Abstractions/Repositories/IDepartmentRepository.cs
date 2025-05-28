using Networkie.Entities;

namespace Networkie.Persistence.Abstractions.Repositories;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<Department?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Department>> GetByContainsNameAsync(string name, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Department>> GetDepartmentsAsPagedForAdmin(int pageIndex = 0, int pageSize = 25,
        string? search = null, CancellationToken cancellationToken = default);

    Task<long> GetDepartmentsAsPagedTotalCountForAdmin(string? search = null,
        CancellationToken cancellationToken = default);
}