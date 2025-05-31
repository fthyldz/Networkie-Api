using Microsoft.EntityFrameworkCore;
using Networkie.Entities;
using Networkie.Persistence.Abstractions.Repositories;
using Networkie.Persistence.EntityFrameworkCore.Contexts;

namespace Networkie.Persistence.EntityFrameworkCore.Repositories;

public class DistrictRepository(IEfCoreDbContext context) : Repository<District>(context), IDistrictRepository
{
    public async Task<IEnumerable<District>> GetAllByCityIdAsync(Guid cityId,
        CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.Where(s => s.CityId == cityId).ToListAsync(cancellationToken);

    public async Task<District?> GetByCityIdAndNameAsync(Guid cityId, string name,
        CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.FirstOrDefaultAsync(s => s.CityId == cityId && s.Name == name,
            cancellationToken);

    public async Task<IEnumerable<District>> GetByContainsNameAsync(string name,
        CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.Where(c => EF.Functions.ILike(c.Name, $"%{name}%")).ToListAsync(cancellationToken);
}