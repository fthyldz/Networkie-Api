using Microsoft.EntityFrameworkCore;
using Networkie.Entities;
using Networkie.Persistence.Abstractions.Repositories;
using Networkie.Persistence.EntityFrameworkCore.Contexts;

namespace Networkie.Persistence.EntityFrameworkCore.Repositories;

public class UniversityRepository(IEfCoreDbContext context) : Repository<University>(context), IUniversityRepository
{
    public async Task<University?> GetByNameAsync(string name, CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.FirstOrDefaultAsync(u => u.Name.ToLower() == name.ToLower(), cancellationToken);

    public async Task<IEnumerable<University>> GetByContainsNameAsync(string name,
        CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.Where(u => u.Name.ToLower().Contains(name.ToLower())).ToListAsync(cancellationToken);

    public async Task<IEnumerable<University>> GetUniversitiesAsPagedForAdmin(int pageIndex = 0, int pageSize = 25, string? search = null, CancellationToken cancellationToken = default) => await TableAsNoTracking.Where(u => string.IsNullOrWhiteSpace(search) || u.Name.ToLower().Contains(search.ToLower())).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken);

    public async Task<long> GetUniversitiesAsPagedTotalCountForAdmin(string? search = null, CancellationToken cancellationToken = default) => await TableAsNoTracking.Where(u => string.IsNullOrWhiteSpace(search) || u.Name.ToLower().Contains(search.ToLower())).CountAsync(cancellationToken);
}