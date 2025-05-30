using Microsoft.EntityFrameworkCore;
using Networkie.Entities;
using Networkie.Persistence.Abstractions.Repositories;
using Networkie.Persistence.EntityFrameworkCore.Contexts;

namespace Networkie.Persistence.EntityFrameworkCore.Repositories;

public class SocialPlatformRepository(IEfCoreDbContext context) : Repository<SocialPlatform>(context), ISocialPlatformRepository
{
    public async Task<IEnumerable<SocialPlatform>> GetByContainsNameAsync(string name,
        CancellationToken cancellationToken = default) =>
        await TableAsNoTracking.Where(p => EF.Functions.ILike(p.Name, $"%{name}%")).ToListAsync(cancellationToken);

    public async Task<IEnumerable<SocialPlatform>> GetSocialPlatformsAsPagedForAdmin(int pageIndex = 0, int pageSize = 25, string? search = null, CancellationToken cancellationToken = default) => await TableAsNoTracking.Where(u => string.IsNullOrWhiteSpace(search) || EF.Functions.ILike(u.Name, $"%{search}%")).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken);

    public async Task<long> GetSocialPlatformsAsPagedTotalCountForAdmin(string? search = null, CancellationToken cancellationToken = default) => await TableAsNoTracking.Where(u => string.IsNullOrWhiteSpace(search) || EF.Functions.ILike(u.Name, $"%{search}%")).CountAsync(cancellationToken);
}