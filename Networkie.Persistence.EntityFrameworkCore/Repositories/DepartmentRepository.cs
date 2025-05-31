using Microsoft.EntityFrameworkCore;
using Networkie.Entities;
using Networkie.Persistence.Abstractions.Repositories;
using Networkie.Persistence.EntityFrameworkCore.Contexts;

namespace Networkie.Persistence.EntityFrameworkCore.Repositories;

public class DepartmentRepository(IEfCoreDbContext context) : Repository<Department>(context), IDepartmentRepository
{
    public async Task<Department?> GetByNameAsync(string name, CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.FirstOrDefaultAsync(d => EF.Functions.ILike(d.Name, $"%{name}%"), cancellationToken);

    public async Task<IEnumerable<Department>> GetByContainsNameAsync(string name,
        CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.Where(c => EF.Functions.ILike(c.Name, $"%{name}%")).ToListAsync(cancellationToken);
    
    public async Task<IEnumerable<Department>> GetDepartmentsAsPagedForAdmin(int pageIndex = 0, int pageSize = 25, string? search = null, CancellationToken cancellationToken = default) => await TableAsNoTracking.Where(d => string.IsNullOrWhiteSpace(search) || EF.Functions.ILike(d.Name, $"%{search}%")).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken);

    public async Task<long> GetDepartmentsAsPagedTotalCountForAdmin(string? search = null, CancellationToken cancellationToken = default) => await TableAsNoTracking.Where(d => string.IsNullOrWhiteSpace(search) || EF.Functions.ILike(d.Name, $"%{search}%")).CountAsync(cancellationToken);
}