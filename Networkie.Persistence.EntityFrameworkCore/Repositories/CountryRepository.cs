using Microsoft.EntityFrameworkCore;
using Networkie.Entities;
using Networkie.Persistence.Abstractions.Repositories;
using Networkie.Persistence.EntityFrameworkCore.Contexts;

namespace Networkie.Persistence.EntityFrameworkCore.Repositories;

public class CountrytRepository(IEfCoreDbContext context) : Repository<Country>(context), ICountryRepository
{
    public async Task<Country?> GetByNameAsync(string name, CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.FirstOrDefaultAsync(c => c.Name == name, cancellationToken);

    public async Task<IEnumerable<Country>> GetByContainsNameAsync(string name,
        CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.Where(c => EF.Functions.ILike(c.Name, $"%{name}%")).ToListAsync(cancellationToken);
    
    public async Task<IEnumerable<Country>> GetCountriesAsPagedForAdmin(int pageIndex = 0, int pageSize = 25, string? search = null, CancellationToken cancellationToken = default) => await TableAsNoTracking.Where(u => string.IsNullOrWhiteSpace(search) || EF.Functions.ILike(u.Name, $"%{search}%")).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken);

    public async Task<long> GetCountriesAsPagedTotalCountForAdmin(string? search = null, CancellationToken cancellationToken = default) => await TableAsNoTracking.Where(u => string.IsNullOrWhiteSpace(search) || EF.Functions.ILike(u.Name, $"%{search}%")).CountAsync(cancellationToken);
}